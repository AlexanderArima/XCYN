db.getCollection('inventory').find(
{
    qty:{$mod:[10,0]}//查询模上10余0的数
})

db.getCollection('inventory').find({
    'item.name':{$regex:/ab/}
})

db.getCollection('inventory').find({$where:function(){
        //where条件筛选
        return this.qty == 15;
}})


