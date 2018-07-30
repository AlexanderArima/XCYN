db.getCollection('inventory').find({})

db.runCommand(

    {

        group:{

             ns: 'inventory',//分组集合名称

             key: { description: 1},//分组的列名

             cond: { _id: { $gt: 1 } },//筛选条件
             
             initial: { count: 0,sum : 0 ,avg:0},//初始化显示的结果集

             $reduce: function ( curr, result ) { 
                 //curr表示每行数据，result表示最终显示的结果

                 result.sum += curr.instock;

                 result.count++;

             },

             finalize:function(result){
                 //对最终的结果进行处理

                 result.avg = result.sum / result.count;

             }


        }

    }

).retval;