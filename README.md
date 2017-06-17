<h2 align="center">
	一个犀利的 HTML 解析器 —— Less.Html
</h2>
<p align="left">
	我写了这个解析器之后才知道，原来 C# 写的 html 解析器有很多。但是因为我没有参照别人的做法，Less.Html 有一个特点，就是它的用法是最接近 jQuery 的。我刻意模仿了 jQuery。比如我写的 test1：
</p>
<pre class="brush:csharp">    
    var q = HtmlParser.Query(testHtml);
    foreach (Element i in q("td"))
    {
        if (!q(i).find("table").hasElement)
            Console.WriteLine(i.textContent);
    }
</pre>
<p align="left">
	解析之后返回的 q 对象，等同于 jQuery 的 $，因为 C# 不支持用 $ 做变量名，所以我用 q 代替。foreach 循环的部分，在 jQuery 的写法应该是 for(var i = 0; i &lt;&nbsp;$("td").length; i++) ，其实在 Less.Html 里面，同样可以这样写，jQuery 不能使用foreach 的原因是，它会枚举对象的属性，C# 没有这个烦恼，所以我做了些改进。if 条件部分，q 和 jQuery 的 $ 一样，是可以传入 Element 类型的，然后调用的 find 方法，作用也是和 jQuery 一样的。hasElement 这个属性，实际上就是 length &gt; 0 的判断，是我写的一个扩展属性，当然，你在 jQuery 里面也可以通过 prototype 做同样的事情。输出部分，textContent 是 HTML DOM 标准的一个方法，作用是获取节点及其后代的文本内容，这和你在编写浏览器运行的 javascript 是一样的。
</p>
<p align="left">
	Less.Html 从解析核心到 DOM、选择器都是我手写的，没有任何依赖项，编译之后只有一个 dll：
</p>
<p align="left">
	<img alt="" src="http://bibaoke.com/img/0HDawfRQu02dKLsw5_nouQ?auth=post" /> 
</p>
<p align="left">
	源代码：<br />
<a href="https://github.com/bibaoke/Less.Html" target="_blank">https://github.com/bibaoke/Less.Html</a><br />
<a href="https://code.csdn.net/closurer/less-html/tree/master" target="_blank">https://code.csdn.net/closurer/less-html/tree/master</a> 
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
	<table cellpadding="5" style="margin:15px 0px 0px;padding:0px;border:1px solid #AAAAAA;border-collapse:collapse;width:90%;color:#000000;font-family:PingFangSC-Regular, Verdana, Arial, 微软雅黑, 宋体;font-size:16px;font-style:normal;font-weight:normal;text-align:left;background-color:#FDFCF8;">
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
					$("*")
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
					$("#lastname")
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
					$(".intro")
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
					$("p")
				</td>
				<td style="border:1px solid #AAAAAA;vertical-align:text-top;">
					所有元素
				</td>
			</tr>
		</tbody>
	</table>
</p>
<p align="left">
	以及它们的组合使用，比如 .class element.class 或者 .class.class，暂时不支持 .class&gt;.class 这种。
</p>
<p align="left">
	因为时间紧迫，加上对我来说够用了，我并没有实现所有的 css3 选择器，不过这是很容易加上去的。<span>所以我想让大家使用 Less.Html，这样我可以得到测试用例，以便更好地改进。<span style="color:#E53333;">只要你有解析 html 的任务，</span></span><span style="color:#E53333;">我可以帮忙写</span><span style="color:#E53333;">，并加入到示例列表中。</span> 
</p>
<p align="left">
	如果你实在不想用&nbsp;Less.Html，我可以介绍一些其他选择：
</p>
<p align="left">
	<a href="https://github.com/zzzprojects/html-agility-pack" target="_blank">Html Agility Pack</a>，这个应该是最早的一个，我当时没有用，是因为网上有人说有容错性的问题，如果一个标签没有结束，解析的结果和浏览器不一致。不过这个项目现在都还有维护，我想应该有所改进的。
</p>
<p align="left">
	<a href="https://github.com/jamietre/CsQuery" target="_blank">CsQuery</a>，两年前就没有维护了。网上说这个项目有很好的 css 选择器。我看了介绍，它使用的是&nbsp;<span style="color:#24292E;font-family:-apple-system, BlinkMacSystemFont, " background-color:#ffffff;"="">Gecko 的解析器，如果是这样的话，可用性和速度都是有保证的。</span> 
</p>
<p align="left">
	<a href="https://github.com/AngleSharp/AngleSharp" target="_blank">AngleSharp</a><span style="line-height:1.6;">，我看了一下介绍，这个项目还有 GUI，应该是功能最强大的一个吧。</span> 
</p>
<p align="left">
	<a href="https://github.com/Ivony/Jumony" target="_blank"><span>Ju</span><span>mony</span></a>，国产。如果我自己没有写的话，我会使用这个，因为有中文的说明。<span id="__kindeditor_bookmark_start_68__"></span> 
</p>