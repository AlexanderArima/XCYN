window.onload = function (event) {
    var array = [3, 6, 9, 8, 11];
    var r = Math.max.apply(null, array);
    //想查找数组中的最大值，可以通过调用Math.max方法，但该方法只能接收多个参数，求出最大值
    //不能接收一个数组，于是我们调用它的apply方法，apply有两个参数，第一个参数是用作方法里this的对象
    //第二个参数是作为传递给max方法的参数数组
    console.log(r);
}

