db.getCollection('School').find({})

db.School.insert({"Address":"武汉市青山区120小区"})

db.School.insertOne({"Name":"钢花小学"})

db.School.insertMany([{"Name":"钢都中学"},{"Address":"武汉市青山区126小区"}])

db.School.insert({"location":[{"long":20},{"lat":10}]})

//嵌套查询
db.School.find({location:{lat:10}});