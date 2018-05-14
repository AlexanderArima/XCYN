//db.getCollection('inventory').find({"item.name":{$eq:"cd"}})

//db.getCollection('inventory').find({qty:{$in:[15]}})

//db.getCollection('inventory').find({qty:{$gte:20}})

//db.getCollection('inventory').find({qty:{$lte:20}})

//db.getCollection('inventory').find({qty:{$nin:[15]}})

//db.getCollection('inventory').find({gty:{$ne:20}})

db.getCollection('inventory').find({$and:[{qty:{$eq:20}},{"item.name":{$eq:"cd"}}]})

db.getCollection('inventory').find({$and:[
    {
        qty:{$not:{$eq:20}},
        "item.name":{$not:{$eq:"ab"}}
    }
]})

db.getCollection('inventory').find({$or:[{qty:{$eq:20}},{"item.name":{$eq:"cd"}}]})

db.getCollection('inventory').find({$nor:[{qty:{$eq:20}},{"item.name":{$eq:"cd"}}]})

db.getCollection('inventory').find()