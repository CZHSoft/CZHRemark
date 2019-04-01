# css基础
## 添加的方式
> * 1.行间样式 style=""  
> * 2.页面级样式 head  <style></style>  
> * 3.外部css文件 head <link rel="stylesheet" type="text/css" href="地址">  
## css选择器类型
> * 1.id选择器:#
> * 2.class选择器:.
> * 3.标签选择器:html标签里style属性
> * 4.通配符选择器:*
> * 5.属性选择器:[属性]  
> * 6.父子选择器:标签或者属性参照
> * 7.直接子元素选择器:a[标签/id/class]>b[标签/id/class]
> * 8.分组选择器: a[标签/id/class],b[标签/id/class]
## 权重计算算法
权重值:!important(正无穷)->行间样式(1000)->id(100)->class|属性|伪类(10)->标签|伪属性(1)->通配符(0)
## css基本标签
font-size:字体大小  
font-weight: 正常或者加粗 strong的用法  
font_style:斜体 em的用法  
font-family:默认arial  
border:边框 三角形  
text-align:字体对齐  
line-height:行间隔  
text-indent:文本缩进 em  
text-decoration:文本修饰，加线条  
cursor:光标值  
hover:伪类，光标选中元素时触发  
## 元素行块分类
> * 1.行级元素 span|strong|em|a|del  
> * 2.块级元素 div|p|ul|ol|li|form|address  
> * 3.行块级元素  img  
### 行块元素的使用
display: inlink|block|inlink-block
### ol ul 的技巧
list-style:none 去掉列表所有符号特性
## 盒子模型
>margin
>>border
>>>padding
>>>>width 
>>>>height
## 层模型: position
> * absolute脱离原来位置定位，最近的父级或者文档进行定位
> * relative 保留原来位置定位，定义为参照物
> * fixed 全局定位
### 其它注意的地方
> * 父级子级的定位margin top 取最大值
> * 触发盒子bfc:1.position:absolute 2.display:inline-block 3.float:left/right 4. overflow:hidden
> * 区域不能共用
> * 只有块级元素看不到浮动元素
> * clear:both 清除浮动模型，让父级包住浮动元素. 块级元素才能生效。
> * ::before|after伪元素:content   行级元素inlink
> * white-space:nowrap 块元素不换行
> * overflow:hidden 内容超出块元素不显示
> * text-overflow:ellipsis 文本溢出变成...
> * background-img background-size background-repeat background-position
