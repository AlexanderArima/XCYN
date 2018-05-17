db.getCollection('inventory').update({"_id":1},{$inc:{qty1:1}}) //自增1

db.getCollection('inventory').update({"_id":1},{$mul:{qty1:2}}) //乘以2

db.getCollection('inventory').update({"_id":1},{$rename:{"qty":"price"}}) //修改列名称

db.getCollection('inventory').update({"_id":1},{$set:{price:1}}) //修改列的值
db.getCollection('inventory').update({"_id":1},{$set:{'item.name':'xc'}}) //创建之前未存在的列

db.getCollection('inventory').update({"_id":6},{$setOnInsert:{'date':'2018-01-01'}},{upsert:true})
