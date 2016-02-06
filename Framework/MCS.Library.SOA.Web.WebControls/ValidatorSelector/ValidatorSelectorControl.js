$HBRootNS.ValidatorSelectorControl = function (element)
{
    $HBRootNS.ValidatorSelectorControl.initializeBase(this, [element]);
    this._inputText = null;
    this._selectorImage = null;
    this._inputHiddenValue = null;

    this._inputTextClientID = "";
    this._selectorImageClientID = "";
    this._inputHiddenValueClientID = "";
    this._selectorImageUrl = "";

    this._value = "";
    this._text = "";
    this._selectorDialogUrl = "";
    this._disabled = false;
};

$HBRootNS.ValidatorSelectorControl.prototype =
{
    initialize: function ()
    {
        $HBRootNS.ValidatorSelectorControl.callBaseMethod(this, 'initialize');
        this._initElements();
    },
    //初始化控件
    _initElements: function ()
    {
        this._inputText = $get(this._inputTextClientID);
        this._selectorImage = $get(this._selectorImageClientID);
        this._inputHiddenValue = $get(this._inputHiddenValueClientID);
        if (this._selectorImage != null)
        {
            $addHandler(this._selectorImage, "click", Function.createDelegate(this, this._selectorImageClick));
        }

        this._setElementsStatus();
    },
    //Validator选择图片点击事件：弹出Validator选择窗口
    _selectorImageClick: function ()
    {
        var sFeature = "dialogWidth:750px; dialogHeight:450px;center:yes;help:no;resizable:yes;scroll:no;status:no";
        var str = this.get_value();
        var arg = null;
        if (str != "")
        {
            arg = Sys.Serialization.JavaScriptSerializer.deserialize(str);
        }
        //需要改造：弹出层方式
        var resultStr = window.showModalDialog(this.get_selectorDialogUrl(), arg, sFeature);
        if (resultStr != null && resultStr != undefined)
        {
            this.set_dataSource(resultStr);
        }
        
    },
    //设置控件
    _setElementsStatus: function ()
    {
        if (this.get_enabled() == true)
        {
            if (this._inputText != null)
            {
                this._inputText.contentEditable = false;
                this._inputText.style.borderWidth = "1px";
            }
            if (this._selectorImage != null)
            {
                this._selectorImage.parentNode.style.display = "";
            }
        }
        else
        {
            if (this._inputText != null)
            {
                this._inputText.contentEditable = false;
                this._inputText.style.borderWidth = "0px";
                
            }

            if (this._selectorImage != null)
            {
                this._selectorImage.parentNode.style.display = "none";
            }
        }
    },

    dispose: function ()
    {
        $HBRootNS.ValidatorSelectorControl.callBaseMethod(this, 'dispose');
    },

    loadClientState: function (value)
    {
        if (value)
        {
            this.set_dataSource(value);
        }
    },

    saveClientState: function ()
    {
        return this.get_dataSource();
    },

    get_value: function ()
    {
        return this._value;
    },

    set_value: function (value)
    {
        this._value = value;

        if (this._inputHiddenValue != null)
        {
            this._inputHiddenValue.value = value;
        }
        this.raiseDataChangedEvent();
    },

    get_text: function ()
    {
        return this._text;
    },

    set_text: function (value)
    {
        this._text = value;
        if (this._inputText != null)
        {
            this._inputText.value = value;
        }
    },

    get_inputTextClientID: function ()
    {
        return this._inputTextClientID;
    },

    set_inputTextClientID: function (value)
    {
        this._inputTextClientID = value;
    },

    get_inputHiddenValueClientID: function ()
    {
        return this._inputHiddenValueClientID;
    },

    set_inputHiddenValueClientID: function (value)
    {
        this._inputHiddenValueClientID = value;
    },

    get_selectorImageClientID: function ()
    {
        return this._selectorImageClientID;
    },

    set_selectorImageClientID: function (value)
    {
        this._selectorImageClientID = value;
    },

    _set_disabled: function (value)
    {
        this._disabled = value;
        this._element.disabled = value;
        this._setElementsStatus();
    },
    //绑定数据:数据格式为JSON
    set_dataSource: function (value)
    {
        if (value == "")
        {
            this.set_value("");
            this.set_text("");
        }
        else
        {
            var data = Sys.Serialization.JavaScriptSerializer.deserialize(value)
            var arrNames = [];
            if (data)
            {
                for (var i = 0; i < data.length; i++)
                {
                    arrNames.push(data[i].Description);
                }
                this.set_text(arrNames.join(","));
            }
            this.set_value(value);
        }
    },
    get_dataSource: function ()
    {
        return this.get_value();
    },
    get_enabled: function ()
    {
        return !this._disabled;
    },

    set_enabled: function (value)
    {
        this._set_disabled(value == false);
    },

    get_selectorDialogUrl: function ()
    {
        return this._selectorDialogUrl;
    },
    set_selectorDialogUrl: function (value)
    {
        this._selectorDialogUrl = value;
    },
    get_selectorImageUrl: function ()
    {
        return this._selectorImageUrl;
    },
    set_selectorImageUrl: function (value)
    {
        this._selectorImageUrl = value;
    },
    //数据Changed事件
    _dataChangedEventKey: "dataChanged",
    add_dataChanged: function (value)
    {
        this.get_events().addHandler(this._dataChangedEventKey, value);
    },
    remove_dataChanged: function (value)
    {
        this.get_events().removeHandler(this._dataChangedEventKey, value);
    },
    //抛出数据Changed处理事件
    raiseDataChangedEvent: function ()
    {
        var handler = this.get_events().getHandler(this._dataChangedEventKey);
        if (handler)
        {
            var e = new Sys.EventArgs;
            handler(this, e);
        }
    }
};

$HBRootNS.ValidatorSelectorControl.registerClass($HBRootNSName + ".ValidatorSelectorControl", $HBRootNS.DialogControlBase);