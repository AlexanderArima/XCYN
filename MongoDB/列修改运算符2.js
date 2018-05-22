//$unset 删除某一列
db.getCollection('inventory').update({_id:5},{
    $unset:{tags:""}
})

//$min 和指定列的值比较，更新最小值
db.getCollection('inventory').update({_id:1},{
    $min:{qty1:100}
})

//$max 和指定列的值比较，更新最大值
db.getCollection('inventory').update({_id:1},{
    $max:{qty1:200}
})

db.getCollection('inventory').update({_id:2},{
    $set:{CreateDate:new Date()}
})

//$currentDate 设置当前的时间或时间戳
db.getCollection('inventory').update({_id:4},{
    $currentDate:{
        CreateDate:true, //时间格式
        CreateDate2:{$type:"timestamp"}, //时间戳
        CreateDate3:{$type:"date"} //时间格式
    } 
})

db.getCollection('inventory').find({})