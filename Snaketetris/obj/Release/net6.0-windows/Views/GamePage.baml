   M S B A M L   `   `   `  ???? F  BSnaketetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Snaketetris.ViewsSnaketetris.Views       Snaketetris.Views.GamePage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:Snaketetris.Views       ? 	Resources   
  Button ?? 
TargetType ?? CornerRadius ?? Padding ?? Color ?? Triggers P? Property P? Value ?? 
TargetName	 ?? Property
 ?? Value??%o  &	        ?? " ??.R??? $??Border??$ 5??$ 2q?$??1q?
???   ?$??20(?$??Verdana)?$??70??$	??200?????? $ #0000FF?????? $ Black??._??? $??2q?$??Center=?$??Center=?$
??True?? P?  ?@J?IsMouseOver$
 true??. ??? $ Border??	 ? ??
 ?#0000CDP?  ? ??$
 True??. ??? $ Border??	 ? ??
 ?#00008B J? InputBindings ?? 
 ?? Key$
 Left?? ?? CommandParameter$ 23?? ?? Command ?? *
KeyCommand+?? $ Up??$ 24?? ?? *
KeyCommand+?? $ Right??$ 25?? ?? *
KeyCommand+?? $
 Down??$ 26?? ?? *
KeyCommand+.??? .+?(? $??	0,10,3,20q?.???? ???? *Snakes+???? .R???  ??Name$ 
GameCanvas?????#101010$	??250??$	??501??$
}?True??????  ?? 
TargetType" ??.??? 	 ?@??Left
 ?? *X+?? 	 ?@??Top
 ?? *Y+???? .R??? 
??????????? *Width+???? *Height+???? *Color+(? $??	0,10,3,20q?.???? ???? *Apples+???? .R??? $ 
GameCanvas??$	??250??$	??501??$
}?True?????? " ??.??? 	 ?@??Left
 ?? *X+?? 	 ?@??Top
 ?? *Y+???? .R?A? ???? *Width+???? *Height+???? *Color+?? $??Start??#??[  $??559,86,29,275q?$??70??$	??200?????? *StartCommand+?? $??Pause??#??[  $??559,228,29,152q?$??70?????? *PauseCommand+?? -    ??Name$ score??$??10,23,606,381q?$??30(?
???   ?$??Verdana)?$
??Bold"????? *Score+ ?? UpdateSourceTrigger$ PropertyChanged=?