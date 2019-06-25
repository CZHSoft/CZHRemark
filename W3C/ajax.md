# ajax基础

## 定义
Ajax 即“ Asynchronous Javascript And XML”（ 异步 JavaScript 和 XML）， 是指一种创建交互式网页应用的网页开发技术。  

## 实现

### 原生
var xhr = new XMLHttpRequest();  
xhr.open("get", url, true);  
xhr.withCredentials = true;  
xhr.onload = function () {  
    console.log(xhr.response);  
};  
xhr.onerror = function () {  
    console.log('error making the request.');  
};  
xhr.onreadystatechange = function () {  
    if (xhr.readyState == 4) {  
        console.log(xhr)  
    }  
}  
xhr.send();  

### jquery
$.ajax({  
    url: "https://a.exp.com",  
    type: "GET",  
    dataType: "jsonp",  
    crossDomain: true,  
    beforeSend: function (xhr) {  
        xhr.setRequestHeader('Access-Control-Allow-Origin:*')  
    },  
    dataFilter: function (json) {  
        console.log("jsonp.filter:" + json);  
        return json;  
    },  
    success: function (data) {  
        console.log(data);  
    },  
    error: function (res) {  
        console.log('error:' + JSON.stringify(res));  
    }  
});  

### axios
const axios = require('axios');  

axios.get('https://a.exp.com')  
    .then(function (response) {  
        // handle success  
        console.log(response);  
    })  
    .catch(function (error) {  
        // handle error  
        console.log(error);  
    })  
    .finally(function () {  
        // always executed  
    });  

## 跨域处理
早期为了防止CSRF（跨域请求伪造）的攻击，浏览器引入了同源策略(SOP)来提高安全性。  

## 方法

### JSONP
JSON with Padding，使用这种技术服务器会接受回调函数名作为请求参数，并将JSON数据填充进回调函数中去。   
$.ajax({  
  url: 'http://www.b.com/getdata?',  
  dataType: 'jsonp',  
  success: function(data) {  
    console.log(data.msg);  
  }  
});  
> * 只支持GET请求
> * 安全性稍低
> * 兼容性非常好

### document.domain
很多大型网站都会使用多个子域名，而浏览器的同源策略对于它们来说就有点过于严格了。如，来自www.a.com想要获取document.a.com中的数据。只要基础域名相同，便可以通过修改document.domain为基础域名的方式来进行通信，但是需要注意的是协议和端口也必须相同。  

document.domain = 'a.com';  
var iframe = document.createElement('iframe');  
iframe.src = 'http://document.a.com';  
iframe.style.display = 'none';  
document.body.appendChild(iframe);  

iframe.onload = function() {  
  var targetDocument = iframe.contentDocument || iframe.contentWindow.document;  
  //可以操作targetDocument  
}  

### window.name
a.com域名下获取b.com域名下的数据，可以通过b域名共享window.name属性通过iframe形式传递。  
例如  
//b.com   
<script>  
var data = {msg: 'hello, world'};  
window.name = JSON.stringify(data);   
</script>  
//a.com  
var iframe = document.createElement('iframe');  
iframe.style.display = 'none';  
document.body.appendChild(iframe);  
var isLoad = false;  
iframe.onload = function() {  
  if(isLoad) {  
    var data = JSON.parse(iframe.contentWindow.name);  
    iframe.contentWindow.document.write('');  
    iframe.contentWindow.close();  
    document.body.removeChild(iframe);  
  }else {  
    iframe.contentWindow.location = 'http://www.b.com/getdata.html';  
    isLoad = true;  
  }  
}  

### window.postMessage
postMessage在处理一些和多页面通信、页面与iframe等消息通信的跨域问题时，有着很好的适用性。  
//a.html  
<iframe id="iframe" src="http://www.domain2.com/b.html" style="display:none;"></iframe>  
<script>         
    var iframe = document.getElementById('iframe');  
    iframe.onload = function() {  
        var data = {  
            name: 'aym'  
        };  
        // 向domain2传送跨域数据  
        iframe.contentWindow.postMessage(JSON.stringify(data), 'http://www.domain2.com');  
    };  
 
    // 接受domain2返回数据  
    window.addEventListener('message', function(e) {  
        alert('data from domain2 ---> ' + e.data);  
    }, false);  
</script>  
 
//b.html  
<script>  
    // 接收domain1的数据  
    window.addEventListener('message', function(e) {  
        alert('data from domain1 ---> ' + e.data);  
 
        var data = JSON.parse(e.data);  
        if (data) {  
            data.number = 16;  
 
            // 处理后再发回domain1  
            window.parent.postMessage(JSON.stringify(data), 'http://www.domain1.com');  
        }  
    }, false);  
</script>  

### 反向代理
通过中间服务器作代理转发。  

### CORS
前面的ajax都是实现cors的代码，现在再添加一个新的。  

fetch('http://www.xxx.com', {  
  method: 'POST',  
  mode: 'cors',  
  credentials: 'include' //接受凭证  
}).then(function(response) {  
  //do something with response  
});  