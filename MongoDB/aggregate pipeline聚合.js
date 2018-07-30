//db.getCollection('inventory').find({})

db.orders.aggregate(
[
    {
        $lookup:
        {
            from:"inventory",
            localField:"item",
            foreignField:"sku",
            as:"new_item"
        }
        
    },
    {
        $match:
        {
           "price":
            {
              $gt:0
            }
        }
    },
    {
        $group:
        {
            _id:"$item",
            totalPrice:{$sum:"$price"},
            countQuantity:{$avg:"$quantity"}
        }
    },
])