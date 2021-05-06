const autocannon = require('autocannon')
const utils = require('./utils')
const command = require("./command")

let abort=false

const stats={
  noId:0,
  created:0,
  updated:0,
  patched:0,
  deleted:0
}

function saveResults(err, result){
  if (abort===true) {
    console.log("Load test cancelled...")
    return
  }
  utils.saveToLowdb(err,{
    ...result,
    stats,
  })
}

const loadTest = autocannon({
  ...utils.settings,
  title:"dotnet-mssql-command",
  requests:[{
      method:'GET',
      path:'/api/v1/commands',
    },
    // For POST/PUT it needs to have setupRequest
    // not sure why, older versions worked without it
    {
      method:'POST',
      setupRequest:(req, context)=>{
        let id=1
        if (context['command_id']){
          id=context['command_id']
        }
        return {
          ...req,
          path:`/api/v1/commands`,
          headers:{
            'content-type':'application/json',
            'autohorization':'Bearer FAKE_JWT_KEY'
          },
          body:JSON.stringify(command.putItem),
        }
      },
      onResponse:(status, body, context)=>{
        if (status === 200) {
          stats.created+=1
          const command = JSON.parse(body)
          if (command && command['id']){
            context['command_id']=command['id']
          }else{
            stats.noId+=1
          }
        }else{
          stats.noId+=1
        }
      }
    },
    {
      method:'PUT',
      setupRequest:(req, context)=>{
        let id=1
        if (context['command_id']){
          id=context['command_id']
        }
        return {
          ...req,
          path:`/api/v1/commands/${id}`,
          headers:{
            'content-type':'application/json',
            'autohorization':'Bearer FAKE_JWT_KEY'
          },
          body:JSON.stringify(command.putItem),
        }
      },
      onResponse:(status, body, context)=>{
        if (status === 204) {
          stats.updated+=1
        }
      }
    },
    {
      method: 'PATCH',
      setupRequest:(req, context)=>{
        let id=1
        if (context['command_id']){
          id=context['command_id']
        }
        return{
          ...req,
          path:`/api/v1/commands/${id}`,
          headers:{
            'content-type':'application/json',
            'autohorization':'Bearer FAKE_JWT_KEY'
          },
          body:JSON.stringify(command.patchItem)
        }
      },
      onResponse:(status, body, context)=>{
        if (status === 204) {
          stats.patched+=1
        }
      }
    },
    {
      method:'GET',
      setupRequest:(req, context)=>{
        let id=1
        if (context['command_id']){
          id=context['command_id']
        }
        return {
          ...req,
          path:`/api/v1/commands/${id}`,
          headers:{
            'content-type':'application/json',
            'autohorization':'Bearer FAKE_JWT_KEY'
          }
        }
      }
    },
    {
      method:"DELETE",
      setupRequest:(req, context)=>{
        // let id=0
        if (context['command_id']){
          id=context['command_id']
          return {
            ...req,
            path:`/api/v1/commands/${id}`,
            headers:{
              'content-type':'application/json',
              'autohorization':'Bearer FAKE_JWT_KEY'
            }
          }
        }else{
          stats.noId+=1
          return {
            method:"GET",
            path:'/api/v1/commands'
          }
        }
      },
      onResponse:(status, body, context)=>{
        if (status === 200) {
          stats.deleted+=1
        }
      }
    }
  ]
},saveResults)

process.once('SIGINT',()=>{
  abort = true
  loadTest.stop()
})

autocannon.track(loadTest)