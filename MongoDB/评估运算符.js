db.getCollection('inventory').find(
{
    qty:{$mod:[10,0]}//��ѯģ��10��0����
})

db.getCollection('inventory').find({
    'item.name':{$regex:/ab/}
})

db.getCollection('inventory').find({$where:function(){
        //where����ɸѡ
        return this.qty == 15;
}})


