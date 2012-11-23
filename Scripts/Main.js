var remote=null;
function rs(n,u,w,h,x) {
  args="width="+w+",height="+h+",resizable=yes,scrollbars=yes,status=0"+x;
  
  remote=window.open(u,n,args);
  if (remote != null) {
    if (remote.opener == null)
      remote.opener = self;
      remote.focus();
  }
  if (x == 1) { return remote; }
}
function OpenDocWnd(u,n) 
{
	rs(n,u,640,480);
}
function ViewDoc(sid)
{
	OpenDocWnd("../Documents/DocTextView_Frameset.aspx?id=" + sid, "View"+sid);
}
function OpenEditDocWnd(u,n) 
{
	rs(n,u,750,500,",left=10,top=10");
}
