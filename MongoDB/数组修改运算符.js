//$�޸�ƥ��ĵ�һ��Ԫ��
db.students.updateOne({_id:1,'grades':80},{$set:{'grades.$':90}});
db.getCollection('students').find({})

//$�޸�ƥ��ĵ�һ��Ԫ��
db.students.updateOne(
    {
        _id:4,grades:{$elemMatch:{grade:{$lte:90},mean:{$gt:80}}}
    },
    {
        $set:{'grades.$.std':10}
    });
    
db.getCollection('students').find({_id:4})

//$[]ռλ����ƥ�����е�Ԫ��
db.students.update(
{
    _id:4
},
{
    $inc:{'grades.$[].grade':100}//�����е�grade+100
},
{
    multi:true
}
);

//$addToSet���Ԫ�ش�������ӣ������������
db.students.update(
{
    _id:1
},
{
    $addToSet:{grades:85}
})
db.getCollection('students').find({_id:1})

//$pop�����ģ���ջ������������ֵΪ1ʱ�Ƴ����һ��Ԫ�أ�������ֵΪ-1ʱ�Ƴ���һ��Ԫ��
db.students.update(
{
    _id:1
},
{
   $pop:{grades:1} 
});
db.getCollection('students').find({_id:1})

//pull���������Ƴ������е�Ԫ��
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
    multi:true //�����˼��������Ҫ��Ȼֻ���޸�һ��
});
db.getCollection('stores').find({})


