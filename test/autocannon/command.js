module.exports={
  postItem:{
    "howto": "testing adding new item",
    "command": "command value 2",
    "platform": "platform value 2"
  },
  putItem:{
    "howto": "testing updating an item",
    "command": "command value update",
    "platform": "platform value update"
  },
  patchItem:[{
    "op":"replace",
    "path":"/howto",
    "value":"pathed how to value"
  }]
}