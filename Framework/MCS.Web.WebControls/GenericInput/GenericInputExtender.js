
// -------------------------------------------------
// Assembly	：	MCS.Web.WebControls
// FileName	：	GenericInputExtender.js
// Remark	：  通用录入控件Extender形式,客户端脚本
// -------------------------------------------------
// VERSION  	AUTHOR		DATE			CONTENT
// 1.0		张曦	    20070815		创建
// -------------------------------------------------

$HGRootNS.GenericInput = function(element) {
    /// <param name="element" domElement="true">The DOM element the behavior is associated with.</param>
    $HGRootNS.GenericInput.initializeBase(this, [element]);
    this._dropArrowImageUrl = null;//箭头图标的地址，现在没有暴露这个属性出来
    this._dropArrowWidth = "16px";//箭头图标的宽度，现在没有暴露这个属性出来
    this._dropArrowBackgroundColor = "#C6E1FF";//箭头部分的背景色
    this._itemFontColor = "#003399";//每个选择项目的字体颜色
    this._itemHoverBackgroundColor = "#FFE6A0";//鼠标移动到选择项目上后的背景颜色
    this._itemHoverFontColor = "#003399";//鼠标移动到选择项目上后的字体颜色
    this._highlightBorderColor = "#2353B2";//控件的边框颜色
    this._highlightBorderTopWidth = 1;
    this._highlightBorderLeftWidth = 1;
    this._highlightBorderRightWidth = 1;
    this._highlightBorderBottomWidth = 1;
    this._items = [];//保存选择项目集合
    this._selectIndex = -1;//当前选中的项目索引值
    this._autoPostBack = false;//是否自动提交
    this._text = "";//当前控件手工输入的文本
    this._divHeight = "";//弹出框的高度
    this._GenericInputPopup = null;//弹出窗口对象
    this._matchCase = false;//是否区分大小写
    this._itemCssClass = "";//选择项目的风格
    this._itemHoverCssClass = "";//选择项目高亮的风格
    this._elementY = 0;
    this._isReadOnly = false;
    this._extenderObject = null;
    this._dropWrapper = null;
    
    //下拉按钮操作处理事件
    this._dropWrapper$delegates = 
    {
        click : Function.createDelegate(this, this._dropWrapper_onclick),
        focusout : Function.createDelegate(this, this._lostFocus),
        contextmenu : Function.createDelegate(this, this._dropWrapper_oncontextmenu)
    },
    
    //选择项目的处理事件
    this._item$delegates = 
    {
        mouseover : Function.createDelegate(this, this._item_onmouseover),
        mouseout : Function.createDelegate(this, this._item_onmouseout),
        click : Function.createDelegate(this, this._item_onclick)
    },
   
    //当文本框的文本改变时的操作
    this._textChange = Function.createDelegate(this,this._text_onChange);
    this._textLostFocus = Function.createDelegate(this,this._inputLostFocus);
    this._setSize$delegates = Function.createDelegate(this,this._setSize);
    this._resize$delegates = Function.createDelegate(this,this.drawDropDown);
    $addHandler(element,"change",this._textChange);
    $addHandler(element,"blur", this._textLostFocus);
    $addHandler(element,"resize",this._resize$delegates);
    $addHandler(window,"resize",this._resize$delegates);
}

//---------------------------------------我就素那无耻的分割线---------------------------------------//

$HGRootNS.GenericInput.prototype = {

	//箭头图标的地址，现在没有暴露这个属性出来
	get_dropArrowImageUrl: function () {
		return this._dropArrowImageUrl;
	},
	set_dropArrowImageUrl: function (value) {
		if (this._dropArrowImageUrl != value) {
			this._dropArrowImageUrl = value;
			if (this.get_isInitialized()) {
				if (this._dropArrow.className) {
					this._dropArrow.className = "";
					this._dropArrowWrapper.style.display = 'block';
				}
				this._dropArrowImage.src = value;
			}
			this.raisePropertyChanged("dropArrowImageUrl");
		}
	},

	//箭头图标的宽度，现在没有暴露这个属性出来
	get_dropArrowWidth: function () {
		return this._dropArrowWidth;
	},
	set_dropArrowWidth: function (value) {

		if (this._dropArrowWidth != value) {
			this._dropArrowWidth = value;
			if (this.get_isInitialized()) {
				this._dropArrow.style.width = value;
			}
			this.raisePropertyChanged("dropArrowWidth");
		}
	},

	//箭头部分的背景色
	get_dropArrowBackgroundColor: function () {
		return this._dropArrowBackgroundColor;
	},
	set_dropArrowBackgroundColor: function (value) {
		if (this._dropArrowBackgroundColor != value) {
			this._dropArrowBackgroundColor = value;
			if (this.get_isInitialized()) {
				this._dropArrow.style.backgroundColor = value;
			}
			this.raisePropertyChanged("dropArrowBackgroundColor");
		}
	},

	//控件的边框颜色
	get_highlightBorderColor: function () {
		return this._highlightBorderColor;
	},
	set_highlightBorderColor: function (value) {
		if (this._highlightBorderColor != value) {
			this._highlightBorderColor = value;
			if (this.get_isInitialized()) {
				// this._dropFrame.style.borderColor = value;
			}
			this.raisePropertyChanged("highlightBorderColor");
		}
	},

	//保存选择项目集合
	get_items: function () {
		return this._items;
	},
	set_items: function (value) {
		if (this._items != value) {
			this._items = value;
		}
	},

	//clientstate
	get_clientStateValue: function () {
		return this._clientStateValue;
	},
	set_clientStateValue: function (value) {
		if (this._clientStateValue != value) {
			this._clientStateValue = value;
		}
	},

	//当前选中的项目索引值
	get_selectIndex: function () {
		return this._selectIndex;
	},
	set_selectIndex: function (value) {
		this._selectIndex = value;
	},

	//是否自动提交
	get_autoPostBack: function () {
		return this._autoPostBack;
	},
	set_autoPostBack: function (value) {
		this._autoPostBack = value;
	},

	//每个选择项目的字体颜色
	get_itemFontColor: function () {
		return this._itemFontColor;
	},
	set_itemFontColor: function (value) {
		this._itemFontColor = value;
	},

	//鼠标移动到选择项目上后的字体颜色
	get_itemHoverFontColor: function () {
		return this._itemHoverFontColor;
	},
	set_itemHoverFontColor: function (value) {
		this._itemHoverFontColor = value;
	},

	//鼠标移动到选择项目上后的背景颜色
	get_itemHoverBackgroundColor: function () {
		return this._itemHoverBackgroundColor;
	},
	set_itemHoverBackgroundColor: function (value) {
		this._itemHoverBackgroundColor = value;
	},

	//当前控件手工输入的文本
	get_text: function () {
		return this._text;
	},
	set_text: function (value) {
		this._text = value;
	},

	//弹出框的高度
	get_divHeight: function () {
		return this._divHeight
	},
	set_divHeight: function (value) {
		this._divHeight = value;
	},

	//控件是否只读
	get_isReadOnly: function () {
		return this._isReadOnly;
	},
	set_isReadOnly: function (value) {
		this._isReadOnly = value;

		var elt = this.get_element(); //得到输入框的对象     
		elt.disabled = value;
	},

	get_itemCssClass: function () {
		return this._itemCssClass;
	},
	set_itemCssClass: function (value) {
		this._itemCssClass = value;
	},

	get_itemHoverCssClass: function () {
		return this._itemHoverCssClass;
	},
	set_itemHoverCssClass: function (value) {
		this._itemHoverCssClass = value;
	},

	get_extenderID: function () {
		return this._extenderID;
	},
	set_extenderID: function (value) {
		this._extenderID = value;
	},

	get_highlightBorderTopWidth: function () {
		return this._highlightBorderTopWidth;
	},
	set_highlightBorderTopWidth: function (value) {
		this._highlightBorderTopWidth = value;
	},

	get_highlightBorderLeftWidth: function () {
		return this._highlightBorderLeftWidth;
	},
	set_highlightBorderLeftWidth: function (value) {
		this._highlightBorderLeftWidth = value;
	},

	get_highlightBorderRightWidth: function () {
		return this._highlightBorderRightWidth;
	},
	set_highlightBorderRightWidth: function (value) {
		this._highlightBorderRightWidth = value;
	},

	get_highlightBorderBottomWidth: function () {
		return this._highlightBorderBottomWidth;
	},
	set_highlightBorderBottomWidth: function (value) {
		this._highlightBorderBottomWidth = value;
	},
	//---------------------------------------我就素那无耻的分割线---------------------------------------//

	//控件销毁时执行的操作
	dispose: function () {
		/// <summary>
		/// Disposes this behavior's resources
		/// </summary>
		if (this._GenericInputPopup
            && this._GenericInputPopup.get_popupDocument()
            && this._GenericInputPopup.get_popupDocument().body
            && this._GenericInputPopup.get_popupDocument().body.document.all('listPanl'))//如果存在则释放，很猥琐的代码……
		{
			$clearChildElementHandlers(this._GenericInputPopup.get_popupDocument().body.document.all('listPanl'));
		}
		if (this._GenericInputPopup) {
			this._GenericInputPopup = null;
		}

		$HGRootNS.GenericInput.callBaseMethod(this, "dispose");
	},

	//下拉箭头的部分失去焦点的操作
	_lostFocus: function () {
		this.hide();
	},

	drawDropDown: function () {
		var elt = this.get_element(); //得到输入框的对象 
		if (!this._isOver) {
			//this._isOver = true;
			var bounds = $HGDomElement.getBounds(elt);
			$HGDomElement.setBounds(this._dropFrameTop,
            {
            	x: bounds.x,
            	y: bounds.y - 1,
            	width: bounds.width + 17,
            	height: this._highlightBorderTopWidth
            });
			$HGDomElement.setBounds(this._dropFrameRight,
            {
            	x: bounds.x + bounds.width + 16, //- 1,
            	y: bounds.y,
            	width: this._highlightBorderRightWidth,
            	height: bounds.height
            });
			$HGDomElement.setBounds(this._dropFrameBottom,
            {
            	x: bounds.x,
            	y: bounds.y + bounds.height, // - 1,
            	width: bounds.width + 17,
            	height: this._highlightBorderBottomWidth
            });
			$HGDomElement.setBounds(this._dropFrameLeft,
            {
            	x: bounds.x,
            	y: bounds.y,
            	width: this._highlightBorderLeftWidth,
            	height: bounds.height
            });
			$HGDomElement.setBounds(this._dropArrow,
            {
            	x: bounds.x + bounds.width - 1, // - 17,
            	y: bounds.y, // + 1,
            	width: 17,
            	height: bounds.height// - 2
            });
			this._dropFrameTop.style.backgroundColor = this._highlightBorderColor;
			this._dropFrameRight.style.backgroundColor = this._highlightBorderColor;
			this._dropFrameBottom.style.backgroundColor = this._highlightBorderColor;
			this._dropFrameLeft.style.backgroundColor = this._highlightBorderColor;
			Sys.UI.DomElement.setVisible(this._dropFrame, true);
			if (!this._oldBackgroundColor) {
				this._oldBackgroundColor = $HGDomElement.getCurrentStyle(elt, 'backgroundColor');
			}
			elt.style.backgroundColor = this._highlightBackgroundColor;
		}
	},

	//控件初始化
	initialize: function () {
		$HGRootNS.GenericInput.callBaseMethod(this, 'initialize');
		var elt = this.get_element(); //得到输入框的对象     

		if (elt.readOnly || elt.tagName == "SPAN")
			this.set_isReadOnly(true);

		var parent = elt.parentNode;
		//绘制GenericInput的边框及下拉箭头
		$HGDomElement.createElementFromTemplate({
			parent: parent,
			nameTable: this,
			name: "_dropFrame",
			nodeName: "span",
			visible: false,
			children: [
            {
            	name: "_dropFrameTop",
            	nodeName: "div",
            	cssClasses: ["GenericInput_frame_line"]
            }, {
            	name: "_dropFrameRight",
            	nodeName: "div",
            	cssClasses: ["GenericInput_frame_line"]
            }, {
            	name: "_dropFrameBottom",
            	nodeName: "div",
            	cssClasses: ["GenericInput_frame_line"]
            }, {
            	name: "_dropFrameLeft",
            	nodeName: "div",
            	cssClasses: ["GenericInput_frame_line"]
            }, {
            	name: "_dropArrow",
            	nodeName: "div",
            	cssClasses: (!this._dropArrowImageUrl) ? ["GenericInput_arrow", "GenericInput_arrow_image"] : ["GenericInput_arrow"],
            	properties:
                {
                	id: this.get_id() + "_dropArrow",
                	style:
                    {
                    	width: this._dropArrowWidth,
                    	backgroundColor: this._dropArrowBackgroundColor
                    }
                },
            	children: [
                {
                	name: "_dropArrowWrapper",
                	nodeName: "div",
                	visible: !!this._dropArrowImageUrl,
                	cssClasses: ["GenericInput_arrow_wrapper"],
                	children: [
                    {
                    	name: "_dropArrowImage",
                    	nodeName: "img",
                    	properties:
                        {
                        	src: this._dropArrowImageUrl
                        }
                    }]
                }]
            }]
		});

		this._dropWrapper = $HGDomElement.createElementFromTemplate({
			parent: null,
			nameTable: this,
			name: "_dropWrapper",
			nodeName: "span",
			properties:
            {
            	style:
                {
                	cursor: "default"
                }
            },
			events: this._dropWrapper$delegates,
			content: $get(this.get_id() + "_dropArrow")//elt,            
		});

		//this.drawDropDown();
		//Sys.Application.add_load(this._resize$delegates);
		window.setTimeout(this._resize$delegates, 0);

		//设置默认值
		for (var i = 0; i < this._items.length; i++) {
			if (this._items[i].Selected)//如果是选中的
			{
				this._selectIndex = i;
				elt.value = this._items[i].Text;
				break;
			}
		}

		this._extenderObject = this;
	},

	_dropWrapper_onclick: function (e) {
		if (e.target.tagName != "A") {
			if (!this.get_isReadOnly())
				this.show();
		}
	},

	_dropWrapper_oncontextmenu: function (e) {
		if (e.target.tagName != "A") {
			if (!this.get_isReadOnly())
				this.show();
		}
		return false;
	},

	//设置弹出窗口的高度，最高为260，小于260自适应，为什么是260呢？因为250不好听
	_setSize: function (popupwin, e) {
		if (e.height > 260) {
			this._divHeight = 260;
			e.height = 260;
		}
		else {
			this._divHeight = "";
		}
		popupwin.get_popupDocument().body.document.all('listPanl').style.height = this._divHeight;

		this._elementY = 0;
		this._getElementTop(this.get_element());
		var eleY = window.screenTop + this._elementY + this.get_element().clientHeight + e.height - document.body.scrollTop;
		//alert(aa);
		if (eleY > window.screen.availHeight) {
			e.y = 0 - e.height;
		}
	},

	_getElementTop: function (value) {
		if (value != null) {
			this._elementY = this._elementY + value.offsetTop;
			this._getElementTop(value.offsetParent);
		}
	},

	//显示popup
	show: function () {
		var elt = this.get_element(); //得到输入框的对象 
		var bounds = $HGDomElement.getBounds(elt);
		var iWidth = bounds.width + 17;
		this._GenericInputPopup = $create($HGRootNS.PopupControl, { width: iWidth, positionElement: this.get_element(), positioningMode: $HGRootNS.PositioningMode.BottomLeft }, { beforeShow: this._setSize$delegates }, {}, null);
		$HGDomElement.set_currentDocument(this._GenericInputPopup.get_popupDocument());
		$HGDomElement.set_currentDocument(document);
		this.createPopupDocument();

		this._GenericInputPopup.show();
	},

	//隐藏popup
	hide: function () {
		if (this._GenericInputPopup) {
			this._GenericInputPopup.hide();
		}
	},

	createPopupDocument: function () {
		var elt = this.get_element(); //得到输入框的对象 
		var bounds = $HGDomElement.getBounds(elt);
		var iWidth = bounds.width + 17;

		this._GenericInputPopup.get_popupBody().innerHTML = "";

		//当高度超过260后要在Style中限制死高度，所以if判断了一下写了两个，区别只是多了一个限定高度
		if (this._divHeight && this._divHeight == "") {
			var listPanl = $HGDomElement.createElementFromTemplate(
            {
            	nodeName: "div",
            	properties:
                {
                	border: 1,
                	id: "listPanl",
                	style:
                    {
                    	overflow: "auto",
                    	width: iWidth,
                    	border: "1px solid " + this._highlightBorderColor
                    }
                }
            },
            this._GenericInputPopup.get_popupBody(),
            null,
            this._GenericInputPopup.get_popupDocument()
            );
		}
		else {//这个里面无耻的限定了DIV的高度
			var listPanl = $HGDomElement.createElementFromTemplate(
                {
                	nodeName: "div",
                	properties:
                    {
                    	border: 1,
                    	id: "listPanl",
                    	style:
                        {
                        	overflow: "auto",
                        	height: this._divHeight, //就是这里，恩，非常无耻
                        	width: iWidth,
                        	border: "1px solid " + this._highlightBorderColor
                        }
                    }
                },
                this._GenericInputPopup.get_popupBody(),
                null,
                this._GenericInputPopup.get_popupDocument()
            );
		}

		this.createListItem(listPanl);
	},

	//绘制选项列表的Item
	createListItem: function (listPanl) {
		var cssClass = "GenericInput_ContextMenuItem";
		var hoverCssClass = "GenericInput_ContextMenuItemhover";

		if (this._itemCssClass != "") {
			cssClass = this._itemCssClass;
		}

		if (this._itemHoverCssClass != "") {
			hoverCssClass = this._itemHoverCssClass;
		}

		this._GenericInputPopup.createListItem(listPanl, cssClass, hoverCssClass, this._item$delegates, this._items, this._selectIndex);
		/*
		if(this._items) 
		{
		for(var i = 0; i < this._items.length; i++) 
		{
		var itemDiv = $HGDomElement.createElementFromTemplate(
		{
		nodeName : "div",
		events : this._item$delegates,
		cssClasses : ["ContextMenuItem"],
		properties : 
		{
		id : "div_Item_" + i,
		indexValue : i,//这个属性非常酷的保存了每个Item的Index值
		border : 1,
		value : "Item value " + this._items[i].Value,
		style : 
		{
		margin : "1px 0 1px 0",
		fontSize : "14px",
		display : "block",
		color: this._itemFontColor
		},
		innerText : this._items[i].Text
		}
		},listPanl,null,
		this._GenericInputPopup.get_popupDocument());
		}
		}
        
		if(this._itemCssClass)
		{
		for(var i = 0; i < this._items.length; i++) 
		{
		var selectedItem = this._GenericInputPopup.get_popupDocument().all("div_Item_" + this._selectIndex);
		if(selectedItem)
		{
		if(this._itemCssClass == "")
		{
		Sys.UI.DomElement.removeCssClass(selectedItem, "ContextMenuItem");
		}
		else
		{
		Sys.UI.DomElement.removeCssClass(selectedItem, this._itemCssClass);
		}
                    
		if(this._itemHoverCssClass == "")
		{
		Sys.UI.DomElement.addCssClass(selectedItem, "ContextMenuItemhover");
                        
		selectedItem.style.color = this.get_itemHoverFontColor();
		selectedItem.style.borderColor = this.get_itemHoverBackgroundColor();
		selectedItem.style.backgroundColor = this.get_itemHoverBackgroundColor();
		}
		else
		{
		Sys.UI.DomElement.addCssClass(selectedItem, this._itemHoverCssClass);
		}
		}
		}
		}*/
	},

	//鼠标移动到选择项目上时的操作
	_item_onmouseover: function (e) {
		var selectedItem = e.target;
		if (this._itemCssClass == "") {
			Sys.UI.DomElement.removeCssClass(selectedItem, "GenericInput_ContextMenuItem");
		}
		else {
			Sys.UI.DomElement.removeCssClass(selectedItem, this._itemCssClass);
		}

		if (this._itemHoverCssClass == "") {
			Sys.UI.DomElement.addCssClass(selectedItem, "GenericInput_ContextMenuItemhover");
		}
		else {
			Sys.UI.DomElement.addCssClass(selectedItem, this._itemHoverCssClass);
		}

		e.stopPropagation();
	},

	//鼠标移出选择项目上时的操作
	_item_onmouseout: function (e) {
		if (this._itemHoverCssClass == "") {
			Sys.UI.DomElement.removeCssClass(e.target, "GenericInput_ContextMenuItemhover");
		}
		else {
			Sys.UI.DomElement.removeCssClass(e.target, this._itemHoverCssClass);
		}

		if (this._itemCssClass == "") {
			Sys.UI.DomElement.addCssClass(e.target, "GenericInput_ContextMenuItem");
		}
		else {
			Sys.UI.DomElement.addCssClass(e.target, this._itemCssClass);
		}

		e.stopPropagation();
	},

	// 加载ClientState
	//     ClientState中保存的是一个长度为2的一维数组
	//         第一个为选中项目的索引值，如果没有选中则为-1
	//         第二个为选中项目的文本，或者输入的文本
	loadClientState: function (value) {
		if (value) {
			var elt = this.get_element(); //$get(this._clientTextControlID);//得到输入框的对象
			var fsArray = Sys.Serialization.JavaScriptSerializer.deserialize(value);

			if (fsArray && fsArray.length > 0) {
				this.set_selectIndex(fsArray[0]);
				if (fsArray.length > 1 && fsArray[1]) {
					this.set_text(fsArray[1]);
				}
				else {
					this.set_text("");
				}

				if (fsArray.length > 2 && fsArray[2]) {
					this.set_items(fsArray[2])
				}
				else {
					this.set_items(null)
				}
			}
			else {
				this.set_selectIndex(-1);
			}

			if (this.get_text() != "")
				elt.value = this.get_text(); //this._items[value].Text;            
		}
	},

	// 保存ClientState
	//     ClientState中保存的是一个长度为2的一维数组
	//         第一个为选中项目的索引值，如果没有选中则为-1
	//         第二个为选中项目的文本，或者输入的文本
	saveClientState: function () {
		//var fsCS = {this.get_selectIndex(), this.get_text()};
		var elt = this.get_element();
		this.set_text(elt.value);
		var fsCS = new Array(3);
		fsCS[0] = this.get_selectIndex();
		fsCS[1] = this.get_text();
		if (this._items.length > 0)
			fsCS[2] = this.get_items();
		else
			fsCS[2] = null;
		return Sys.Serialization.JavaScriptSerializer.serialize(fsCS);
	},

	//当选择项目被点击的时候的操作
	_item_onclick: function (e) {
		var elt = this.get_element(); //得到输入框的对象 

		if (elt.onSelectItem) {
			if (!eval(elt.onSelectItem)) {
				return; //取消操作
			}
		}
		elt.value = e.target.innerText;
		this.set_text(e.target.innerText);
		if (this.get_selectIndex() != e.target.indexValue) {
			this.set_selectIndex(e.target.indexValue);
			if (elt.onChange) {
				eval(elt.onChange);
			}
			if (elt.onSelectedItem) {
				eval(elt.onSelectedItem);
			}
			if (this.get_autoPostBack()) {//如果是自动提交则提交
				eval(elt.onItemClick);
			}
		}
		this.hide();
	},

	_text_onChange: function (e) {
		var elt = this.get_element(); //得到输入框的对象 
		this.set_selectIndex(-1); //手工输入，则不设置索引
		this.set_text(elt.value); //保存手工输入的信息c 
		eval(elt.onChange); //客户端onChange事件
		if (this.get_autoPostBack()) {//如果是自动提交则提交
			eval(elt.onTextChange);
		}
	},

	_inputLostFocus: function (e) {
		//输入的内容是否出现在选择项目中
		for (var i = 0; i < this._items.length; i++) {
			if (this._matchCase)//区分大小写
			{
				if (this._items[i].Text == this.get_element().value)//如果是选中的
				{
					this._selectIndex = i;
					break;
				}
			}
			else//不区分大小写
			{
				if (this._items[i].Text.toLowerCase() == this.get_element().value.toLowerCase())//如果是选中的
				{
					this._selectIndex = i;
					break;
				}
			}
		}
	}
}

$HGRootNS.GenericInput.registerClass($HGRootNSName + ".GenericInput", $HGRootNS.BehaviorBase);