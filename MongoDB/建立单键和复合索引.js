db.getCollection('School').find({})

var arr = [];
for(var i = 0;i < 10000;i++)
{
    var name = "jack" + i;
    var age = parseInt((Math.random() * 100000) % 100);
    arr.push({"name":name,"age":age});
}
print(arr.length);
//db.School.remove({});
db.School.insertMany(arr)
db.School.find({age:1}).explain();
db.School.find({name:"jack1",age:1}).explain();
db.School.createIndex({age:1})//单键索引
db.School.createIndex({name:1,age:1})//复合索引