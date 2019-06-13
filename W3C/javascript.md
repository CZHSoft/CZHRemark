# javascript基础

## 执行上下文
> * 全局上下文，window对象
> * 局部上下文

## 作用域链
> * 1.执行环境决定了变量的生命周期，以及哪部分代码可以访问其中变量
> * 2.执行环境有全局执行环境（全局环境）和局部执行环境之分。
> * 3.每次进入一个新的执行环境，都会创建一个用于搜索变量和函数的作用域链
> * 4.函数的局部环境可以访问函数作用域中的变量和函数，也可以访问其父环境，乃至全局环境中的变量和环境。
> * 5.全局环境只能访问全局环境中定义的变量和函数，不能直接访问局部环境中的任何数据。
> * 6.变量的执行环境有助于确定应该合适释放内存。

## 提升
> * 变量提升
> * 函数提升

## 闭包定义
内部函数保存到外部时，将生成闭包。闭包导致原作用域链不释放，造成内存泄漏。  

## 闭包作用
> * 1.公有变量
> * 2.缓存
> * 3.实现封装
> * 4.模块化开发

### 公有变量
function foo(x) {  
var tmp = 3; //tmp产生泄漏,公共变量  
return function (y) {  
    alert(x + y + tmp);  
    x.memb = x.memb ? x.memb + 1 : 1;  
    alert(x.memb);  
    }  
}    
var age = new Number(2);   
var bar = foo(age); // bar 现在是一个引用了age的闭包  
bar(10);  
bar(10);  
bar(10);  

### 缓存和封装
var db = (function() {  
// 创建一个隐藏的object, 这个object持有一些数据  
// 从外部是不能访问这个object的  
var data = {};  
// 创建一个函数, 这个函数提供一些访问data的数据的方法  
return function(key, val) {  
    if (val === undefined) { return data[key] } // get  
    else { return data[key] = val } // set  
    }   
// 我们可以调用这个匿名方法  
// 返回这个内部函数，它是一个闭包  
})();  

db('x'); // 返回 undefined  
db('x', 1); // 设置data['x']为1  
db('x'); // 返回 1  
// 我们不可能访问data这个object本身  
// 但是我们可以设置它的成员  

## 原型链
__proto__是实现原型链的关键，而prototype则是原型链的组成。  

## DOM
获取节点：  
document.getElementById(idName)          //通过id号来获取元素，返回一个元素对象  
document.getElementsByName(name)       //通过name属性获取id号，返回元素对象数组  
document.getElementsByClassName(className)   //通过class来获取元素，返回元素对象数组（ie8以上才有）  
document.getElementsByTagName(tagName)       //通过标签名获取元素，返回元素对象数组  
获取/设置元素的属性值：  
element.getAttribute(attributeName)     //括号传入属性名，返回对应属性的属性值  
element.setAttribute(attributeName,attributeValue)    //传入属性名及设置的值  
创建节点Node：  
document.createElement("h3")       //创建一个html元素，这里以创建h3元素为例  
document.createTextNode(String); //创建一个文本节点；  
document.createAttribute("class"); //创建一个属性节点，这里以创建class属性为例  
增添节点：  
element.appendChild(Node);   //往element内部最后面添加一个节点，参数是节点类型  
elelment.insertBefore(newNode,existingNode); //在element内部的中在existingNode前面插入newNode  
删除节点：  
element.removeChild(Node)    //删除当前节点下指定的子节点，删除成功返回该被删除的节点，否则返回null  
常用属性:  
获取当前元素的父节点 ：  
element.parentNode     //返回当前元素的父节点对象  
获取当前元素的子节点：  
element.chlidren        //返回当前元素所有子元素节点对象，只返回HTML节点  
element.chilidNodes   //返回当前元素多有子节点，包括文本，HTML，属性节点。（回车也会当做一个节点）  
element.firstChild      //返回当前元素的第一个子节点对象  
element.lastChild       //返回当前元素的最后一个子节点对象  
获取当前元素的同级元素：  
element.nextSibling          //返回当前元素的下一个同级元素 没有就返回null  
element.previousSibling   //返回当前元素上一个同级元素 没有就返回null  
获取当前元素的文本：  
element.innerHTML   //返回元素的所有文本，包括html代码  
element.innerText     //返回当前元素的自身及子代所有文本值，只是文本内容，不包括html代码  
获取当前节点的节点类型：node.nodeType   //返回节点的类型,数字形式（1-12）常见几个1：元素节点，2：属性节点，3：文本节点。  
设置样式：element.style.color=“#eea”;      //设置元素的样式时使用style，这里以设置文字颜色为例。  

## BOM
var leftPos=(typeof window.screenLeft==='number')?window.screenLeft:window:screenX;  
var topPos=(typeof window.screenLeft==='number')?window.screenTop:window:screenY;  
window.moveTo(0,0);  
window.moveBy(20,10);  
window.resizeTo(100,100);  
window.resizeBy(100,100);  
var pageWith=document.documentElement.clientWidth||document.body.clientWidth;  
var pageHeight=document.documentElement.clientHeight||document.body.clientHeight;  
window.open("url");  
setInterval()  
setTimeout()  
alert()://带有一个确定按钮  
confirm()：//带有一个确定和取消按钮  
prompt()://显示OK和Cancel按钮之外，还会显示一个文本输入域  
location.hash//#contents　　返回url中的hash，如果不包含#后面的内容，则返回空字符串  
location.host//www.wrox.com:80　　返回服务器名称和端口号  
location.port//80 返回端口号  
location.hostname//www.wrox.com　　返回服务器名称  
location.href//http://www.wrox.com　　返回当前加载页面的完整url  
location.pathname// /index.html　　返回url中的目录和文件名  
location.protocol //http　　返回页面使用的协议  
location.search//?q=javascript　　返回url中的查询字符串  
location.href=http://www.baidu.com //改变浏览器的位置  
location.replace('http://www.baidu.com')
location.reload()：//重置当前页面，可能从缓存，也可能从服务器；如果强制从服务器取得，传入true参数  
navigator.userAgent：//用户代理字符串，用于浏览器监测中、  
navigator.plugins://浏览器插件数组，用于插件监测  
navigator.registerContentHandler //注册处理程序，如提供RSS阅读器等在线处理程序。  
history.go(-1);//等价于history.back()  
history.go(1); //等价于 history.forward()  
history.go('wrox.com');




