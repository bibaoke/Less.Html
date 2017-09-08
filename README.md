<h2 align="center">
    一个犀利的 HTML 解析器&nbsp;—— Less.Html
</h2>
<p align="left">
    我写了这个解析器之后才知道，原来 C# 写的 html 解析器有很多。但是因为我没有参照别人的做法，Less.Html 有一个特点，就是它的用法是最接近 jQuery 的。我刻意模仿了 jQuery。比如我写的&nbsp;<a href="http://bibaoke.com/post/75" target="_blank">示例一</a>：
</p>
<pre class="brush:csharp">var q = HtmlParser.Query(testHtml);
foreach (Element i in q("td"))
{
    if (!q(i).find("table").hasElement)
    {
        Console.WriteLine(i.textContent);
    }
}
</pre>
<p align="left">
    解析之后返回的 q 对象，等同于 jQuery 的 $，因为 C# 不支持用 $ 做变量名，所以我用 q 代替。foreach 循环的部分，在 jQuery 的写法应该是 for(var i = 0; i &lt;&nbsp;$("td").length; i++) ，其实在 Less.Html 里面，同样可以这样写，jQuery 不能使用foreach 的原因是，它会枚举对象的属性，C# 没有这个烦恼，所以我做了些改进。if 条件部分，q 和 jQuery 的 $ 一样，是可以传入 Element 类型的，然后调用的 find 方法，作用也是和 jQuery 一样的。hasElement 这个属性，实际上就是 length &gt; 0 的判断，是我写的一个扩展属性，当然，你在 jQuery 里面也可以通过 prototype 做同样的事情。输出部分，textContent 是 HTML DOM 标准的一个方法，作用是获取节点及其后代的文本内容，这和你在编写浏览器运行的 javascript 是一样的。
</p>
<p align="left">
    Less.Html 从解析核心到 DOM、选择器都是我手写的，没有任何依赖项，编译之后只有两个 dll：
</p>
<p align="left">
    <img src="http://bibaoke.com/img/o5lKTxHObUiq-4pLMxnD9Q?auth=post" alt="" />
</p>
<p align="left">
    其中一个是我的基础类库，也是开源的。
</p>
<p align="left">
    示例一代码：<a href="https://github.com/bibaoke/Less.Html/blob/master/Test/Test1.cs" target="_blank">GitHub</a>&nbsp; &nbsp;&nbsp;<a href="https://code.csdn.net/closurer/less-html/tree/master/Test/Test1.cs" target="_blank">CSDN</a>
</p>
<p align="left">
    项目代码：<a href="https://github.com/bibaoke/Less.Html" target="_blank">GitHub</a>&nbsp; &nbsp;&nbsp;<a href="https://code.csdn.net/closurer/less-html/tree/master" target="_blank">CSDN</a>
</p>
<p align="left">
    NuGet:<br />
    <a href="https://www.nuget.org/packages/Less.Html/" target="_blank">https://www.nuget.org/packages/Less.Html</a>
</p>
<p align="left">
    我当时写这个解析器是因为要做网页爬虫，核心三天写完就能用了，并用来解析了几百万个网页，可用性上是没有问题的。后来我觉得 Razor 语法还是不够好，于是我改进了这个解析器，加上了选择器和操作 html 文档的方法，制作了我自己的视图引擎 QPage。我的个人博客就是使用这个引擎写的：
</p>
<p align="left">
    <img alt="" src="http://bibaoke.com/img/fXRNW1C8B06PSAO73f0Sig?auth=post" />
</p>
<p align="left">
    后来我在网上知道了 csQuery、Jumony 这些解析器的作者，都会把自己的解析器做成视图引擎，我开始觉得在服务器端解析文档，而不是拼接文档，应该是未来的趋势。
</p>
<p align="left">
    Less.Html 只支持了几种基本的选择器语法：
</p>
<p align="left">
    <table style="margin:15px 0px 0px;padding:0px;border:1px solid #AAAAAA;border-collapse:collapse;width:90%;color:#000000;font-family:PingFangSC-Regular, Verdana, Arial, 微软雅黑, 宋体;font-size:16px;font-style:normal;font-weight:normal;text-align:left;background-color:#FDFCF8;" cellpadding="5">
        <tbody>
            <tr>
                <th style="border:1px solid #3F3F3F;vertical-align:baseline;background-color:#3F3F3F;text-align:left;color:#FFFFFF;">
                    选择器
                </th>
                <th style="border:1px solid #3F3F3F;vertical-align:baseline;background-color:#3F3F3F;text-align:left;color:#FFFFFF;">
                    实例
                </th>
                <th style="border:1px solid #3F3F3F;vertical-align:baseline;background-color:#3F3F3F;text-align:left;color:#FFFFFF;">
                    选取
                </th>
            </tr>
            <tr>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    *
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    q("*")
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    所有元素
                </td>
            </tr>
            <tr>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    #<i>id</i>
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    q("#lastname")
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    id="lastname" 的元素
                </td>
            </tr>
            <tr>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    .<i>class</i>
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    q(".intro")
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    所有 class="intro" 的元素
                </td>
            </tr>
            <tr>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    <i>element</i>
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    q("p")
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    所有&lt;p&gt;元素
                </td>
            </tr>
            <tr>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    :<i>first</i>
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    q("p:first")
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    第一个&lt;p&gt;元素
                </td>
            </tr>
            <tr>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    :<i>last</i>
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    q("p:last")
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    最后一个&lt;p&gt;元素
                </td>
            </tr>
            <tr>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    [<i>attribute</i>]
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    q("[href]")
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    所有带有href属性的元素
                </td>
            </tr>
            <tr>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    [<i>attribute=value</i>]
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    q("[href='#']")
                </td>
                <td style="border:1px solid #AAAAAA;vertical-align:text-top;">
                    所有href属性的值等于"#"的元素
                </td>
            </tr>
        </tbody>
    </table>
</p>
<p align="left">
    以及它们的组合使用，比如 .class element.class 或者 .class.class，暂时不支持 .class&gt;.class 这种。
</p>
<p align="left">
    因为时间紧迫，加上对我来说够用了，我并没有实现所有的 css3 选择器，而是根据需要逐步添加，添加了新的选择器我会更新上面的表格。
</p>
<p align="left">
    下面是我写的一些使用示例：
</p>
<p align="left">
    <a href="http://bibaoke.com/post/75" target="_blank">示例一：获取嵌套元素中的正确内容</a> <br />
    <a href="http://bibaoke.com/post/76" target="_blank">示例二：以 Less.Html&nbsp;做视图引擎 </a><br />
    <a href="http://bibaoke.com/post/77" target="_blank">示例三：与 WebClient 的配合使用，以抓取 CSDN 论坛内容为例</a><br />
    <a href="http://bibaoke.com/post/78" target="_blank">示例四：与 WebBrowser 的配合使用，以抓取京东手机价格为例</a><br />
    <a href="http://bibaoke.com/post/79" target="_blank">示例五：使用方法详解</a>
</p>
<p align="left">
    如果你实在不想用&nbsp;Less.Html，我可以介绍一些其他选择：
</p>
<p align="left">
    <a href="https://github.com/zzzprojects/html-agility-pack" target="_blank">Html Agility Pack</a>，这个应该是最早的一个，我当时没有用，是因为网上有人说有容错性的问题，如果一个标签没有结束，解析的结果和浏览器不一致。不过这个项目现在都还有维护，我想应该有所改进的。
</p>
<p align="left">
    <a href="https://github.com/jamietre/CsQuery" target="_blank">CsQuery</a>，两年前就没有维护了，网上说这个项目有很好的 css 选择器。我看了介绍，它使用的是&nbsp;Gecko 的解析器，如果是这样的话，可用性和速度都是有保证的。
</p>
<p align="left">
    <a href="https://github.com/AngleSharp/AngleSharp" target="_blank">AngleSharp</a><span style="line-height:1.6;">，我看了一下介绍，这个项目还有 GUI，应该是功能最强大的一个吧。</span>
</p>
<p align="left">
    <a href="https://github.com/Ivony/Jumony" target="_blank">Jumony</a>，国产。如果我自己没有写的话，我会使用这个，因为有中文的说明。
</p>