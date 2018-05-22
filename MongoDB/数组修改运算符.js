//$修改匹配的第一个元素
db.students.updateOne({_id:1,'grades':80},{$set:{'grades.$':90}});
db.getCollection('students').find({})

//$修改匹配的第一个元素
db.students.updateOne(
    {
        _id:4,grades:{$elemMatch:{grade:{$lte:90},mean:{$gt:80}}}
    },
    {
        $set:{'grades.$.std':10}
    });
    
db.getCollection('students').find({_id:4})

//$[]占位符，匹配所有的元素
db.students.update(
{
    _id:4
},
{
    $inc:{'grades.$[].grade':100}//让所有的grade+100
},
{
    multi:true
}
);

//$addToSet如果元素存在则不添加，不存在则添加
db.students.update(
{
    _id:1
},
{
    $addToSet:{grades:85}
})
db.getCollection('students').find({_id:1})

//$pop运算符模拟出栈操作，当它的值为1时移除最后一个元素，当它的值为-1时移除第一个元素
db.students.update(
{
    _id:1
},
{
   $pop:{grades:1} 
});
db.getCollection('students').find({_id:1})

//pull根据条件移除数组中的元素
db.stores.update(
{},
{
    $pull:{fruits:'apples'}
});

db.stores.update(
{},
{
    $pull:{fruits:{$in:['bananas','oranges']}}
},
{
    multi:true //别忘了加上这个，要不然只能修改一行
});
db.getCollection('stores').find({})


