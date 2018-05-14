db.getCollection('inventory').find({
        'tags':{$all:['A','B','C']}//数组中的元素要全部满足该条件
    })

//等价于
db.getCollection('inventory').find({
        $where:function(){
                var flag1 = 0;
                var flag2 = 0;
                var flag3 = 0;
                for(var i = 0;i < this.tags.length;i++){
                        if(this.tags[i] == 'A')
                        {
                            flag1 = 1;
                        }
                        else if(this.tags[i] == 'B')
                        {
                            flag2 = 1;
                        }
                        else if(this.tags[i] == 'C')
                        {
                            flag3 = 1;
                        }
                    }
                if(flag1 == 1 && flag2 == 1 && flag3 == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
    });
    
db.getCollection('inventory').find({
    'tags':{$elemMatch:{$eq:'A',$eq:'B'}}//数组中的元素只要有一个满足条件。
})

//等价于
db.getCollection('inventory').find({
    $where:function(){
        for(var i = 0;i < this.tags.length;i++)
        {
            if(this.tags[i] == 'A')
            {
                return true;
            }
            else if(this.tags[i] == 'B')
            {
                return true;
            }
        }
        return false;
    }
})

db.getCollection('inventory').find({
    tags:{$size:2}//返回数组长度为2的数组
});
    