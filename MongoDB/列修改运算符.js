db.getCollection('inventory').update({"_id":1},{$inc:{qty1:1}}) //����1

db.getCollection('inventory').update({"_id":1},{$mul:{qty1:2}}) //����2

db.getCollection('inventory').update({"_id":1},{$rename:{"qty":"price"}}) //�޸�������

db.getCollection('inventory').update({"_id":1},{$set:{price:1}}) //�޸��е�ֵ
db.getCollection('inventory').update({"_id":1},{$set:{'item.name':'xc'}}) //����֮ǰδ���ڵ���

db.getCollection('inventory').update({"_id":6},{$setOnInsert:{'date':'2018-01-01'}},{upsert:true})
