db.getCollection('School').find({})

db.School.insert({"Address":"�人����ɽ��120С��"})

db.School.insertOne({"Name":"�ֻ�Сѧ"})

db.School.insertMany([{"Name":"�ֶ���ѧ"},{"Address":"�人����ɽ��126С��"}])

db.School.insert({"location":[{"long":20},{"lat":10}]})

//Ƕ�ײ�ѯ
db.School.find({location:{lat:10}});