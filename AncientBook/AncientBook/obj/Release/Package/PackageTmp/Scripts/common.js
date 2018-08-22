
//数组指定移除某一个元素，调用：数组对象.removeByValue(value)
Array.prototype.removeByValue = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) {
            this.splice(i, 1);
            break;
        }
    }
}

// remove item  from old Array
function removeValueFromArray(oldArray,val)
{
    if (oldArray.indexOf(val) > -1) {
        var newArray = new Array();
        for (var i = 0; i < oldArray.length; i++) {
            if (oldArray[i] == val) {
                continue;
            }
            newArray.push(oldArray[i]);
        }
        return newArray;
    }
    return oldArray;
}