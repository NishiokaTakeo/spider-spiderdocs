var Utilities = new function()
{
//---------------------------------------------------------------------------------
    this.Dialog = function(title, msg, func, buttons, width, modal)
	{
		if(width == null)
			width = 'auto';
		
		$('<div></div>').appendTo('body')
			.html('<div>' + msg + '</div>')
			.dialog({
				modal: modal,
				title: title,
				zIndex: 10000,
				autoOpen: true,
				width: width,
				resizable: false,
				buttons: buttons,
				close: function () {
					$(this).remove();
				}
			});
	};

//---------------------------------------------------------------------------------
    this.DialogYesNo = function(title, msg, func)
	{
		var buttons =
			{
				Yes: function () {
					if(func != null)
						func(1);

					$(this).dialog("close");
				},
				No: function () {
					if(func != null)
						func(0);

					$(this).dialog("close");
				}
			};

		this.Dialog(title, msg, func, buttons, null, true);
	};

//---------------------------------------------------------------------------------
    this.DialogNoYes = function(title, msg, func)
	{
		var buttons =
			{
				No: function () {
					if(func != null)
						func(0);

					$(this).dialog("close");
				},			
				Yes: function () {
					if(func != null)
						func(1);

					$(this).dialog("close");
				}
			};

		this.Dialog(title, msg, func, buttons, null, true);
	};

//---------------------------------------------------------------------------------
    this.DialogOK = function(title, msg, func, width, modal)
	{
		if(modal == null)
			modal = true;
		
		var buttons =
			{
				OK: function () {
					if(func != null)
						func();

					$(this).dialog("close");
				}
			};

		this.Dialog(title, msg, func, buttons, width, modal);
	};

//---------------------------------------------------------------------------------
	this.GetToday = function()
	{
		var dt = new Date();
		
		var ans = ('0' + dt.getDate()).slice(-2) + '/' +
                  ('0' + (dt.getMonth() + 1)).slice(-2) + '/' +
                  (dt.getYear() + 1900);

		return ans;
	}

//---------------------------------------------------------------------------------
	this.GetNow = function()
	{
		var dt = new Date();
		
		var ans = ('0' + dt.getHours()).slice(-2) + ':' + 
				  ('0' + dt.getMinutes()).slice(-2);

		return ans;
	}

//---------------------------------------------------------------------------------
	this.StringContains = function(src, word)
    {
	    if(src.indexOf(word) >= 0)
	        return true;
	    else
	        return false;
	}

//---------------------------------------------------------------------------------
};
