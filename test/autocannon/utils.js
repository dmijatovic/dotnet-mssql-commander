// const fs = require('fs')
const ldb = require('lowdb')
const FileSync = require('lowdb/adapters/FileSync')

const adapter = new FileSync('./report/db.json')
const db = ldb(adapter)

module.exports = {
  saveToLowdb:(err, result)=>{
    if (err) {
      console.error(err)
    }else{
      // basic stats
      const {stats} = result
      console.log(`IdNotRetuned: ${stats.noId}`)
      console.log(`Created: ${stats.created}`)
      console.log(`Updated: ${stats.updated}`)
      console.log(`Patched: ${stats.patched}`)
      console.log(`Deleted: ${stats.deleted}`)
      //add
      db.get('report')
        .push(result)
        .write()

      console.log("Saved to lowdb json file")
    }
  },
  // settings
  settings:{
    //default url (actix)
    url:"http://localhost:5000",
    connections:10,
    duration:30,
  },
}