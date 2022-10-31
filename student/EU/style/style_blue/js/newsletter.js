// JavaScript Document
str="";
str+='<dd class="last"><form action="/community/newsletter/maillist-sub.php" id="maillist" name="maillist" method="post"><div class="newsletter">';
str+='   <input name="email" size="23" onclick="if (this.value == \'Your E-mail\') {this.value = \'\';}" onmouseout="if (this.value == \'\') {this.value = \'Your E-mail\';}" value="Your E-mail" type="text" /> </div>';
str+='<div style="float:left; width:38px; "><img src="/images/system/btn_newsletter.gif" width="30" height="18" style="cursor: pointer;" onclick="if(maillist.email.value &amp;&amp; maillist.email.value!=\'Your E-mail\') {maillist.submit();} else {maillist.email.value = \'Your E-mail\';}"></div>';
str+=" </form></dd><br />";
str+="<dd style=\"clear:both;\" class=\"last\">";
str+='Input your e-mail. Get them all.<ul>';
str+='<li>instant 7% discount coupon</li>';
str+='<li>latest tech tips</li>';	
str+='<li>subsequent freebie offers</li>';
str+='<li>product upgrade news</li></ul>' ;   
 str+='</dd>';
document.write(str);