db.getCollection('inventory').find({})

db.runCommand(

    {

        group:{

             ns: 'inventory',//���鼯������

             key: { description: 1},//���������

             cond: { _id: { $gt: 1 } },//ɸѡ����
             
             initial: { count: 0,sum : 0 ,avg:0},//��ʼ����ʾ�Ľ����

             $reduce: function ( curr, result ) { 
                 //curr��ʾÿ�����ݣ�result��ʾ������ʾ�Ľ��

                 result.sum += curr.instock;

                 result.count++;

             },

             finalize:function(result){
                 //�����յĽ�����д���

                 result.avg = result.sum / result.count;

             }


        }

    }

).retval;