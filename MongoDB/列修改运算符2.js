//$unset ɾ��ĳһ��
db.getCollection('inventory').update({_id:5},{
    $unset:{tags:""}
})

//$min ��ָ���е�ֵ�Ƚϣ�������Сֵ
db.getCollection('inventory').update({_id:1},{
    $min:{qty1:100}
})

//$max ��ָ���е�ֵ�Ƚϣ��������ֵ
db.getCollection('inventory').update({_id:1},{
    $max:{qty1:200}
})

db.getCollection('inventory').update({_id:2},{
    $set:{CreateDate:new Date()}
})

//$currentDate ���õ�ǰ��ʱ���ʱ���
db.getCollection('inventory').update({_id:4},{
    $currentDate:{
        CreateDate:true, //ʱ���ʽ
        CreateDate2:{$type:"timestamp"}, //ʱ���
        CreateDate3:{$type:"date"} //ʱ���ʽ
    } 
})

db.getCollection('inventory').find({})