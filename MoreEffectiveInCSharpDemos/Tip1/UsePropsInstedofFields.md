﻿<font face="microsoft yahei">
 
- 要始终使用属性而不是可以访问的数据成员

    - 因为属性可以进行多线程的安全处理
    - 属性可以进行权限设置
    - 属性可以进行错误输入验证
    - 属性具备方法的一切优势, 包括可以声明为virtual和abstract
    - 属性可以创建为索引器

- 隐式属性来表示可变的数据
    - >public string Name {get;set;}
    - 此时编译器会自动补全一个`后援字段`, 在我们自己写的类中想操作这个后援字段也必须通过隐式属性
    - 属性有的好处它都有