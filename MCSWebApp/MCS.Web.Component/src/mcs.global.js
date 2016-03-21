var mcs = mcs || {};

(function () {
    'use strict';

    var getFileName = function (fileType, filePath, isLocal) {
        var fileName = !isLocal ? mcs.config.componentBaseUrl.replace('http://', 'http:\\') : '';
        var extension = '';
        switch (fileType) {
            case mcs.config.fileTypes.css:
                extension += '.' + mcs.config.fileTypes.css;
                break;
            case mcs.config.fileTypes.javascript:
                extension += '.' + mcs.config.fileTypes.javascript;
                break;
        }
        if (!extension) return;

        if (filePath.substring(filePath.length - extension.length) != extension) {
            fileName += filePath + extension;
        } else {
            fileName += filePath;
        }
        fileName = fileName.replace(new RegExp('\/\/', 'gm'), '/').replace('http:\\', 'http://');
        return fileName;
    };

    var handleParam = function (fileType, params) {
        if (!params.length) return;

        var assets = { files: [], localFiles: [], container: '' };

        if (params.length == 1) {
            if (params[0] instanceof Object) {
                assets = params;
            } else if (params[0] instanceof Array) {
                assets.files = params[0];
            } else if (arguments[0] instanceof String) {
                assets.files = [params[0]];
            }
        } else {
            if (params[0] instanceof Array) {
                assets.files = params[0];
            } else if (params[1] instanceof String) {
                assets.files = [params[0]];
            }
            if (params[1] instanceof Array) {
                assets.localFiles = params[1];
            } else if (params[1] instanceof String) {
                assets.localFiles = [params[1]];
            }

            assets.container = document.getElementById(arguments[2]) || '';
        }

        if (fileType == mcs.config.fileTypes.css) {
            return assets[0] || {
                cssFiles: assets.files,
                localCssFiles: assets.localFiles,
                container: assets.container
            };
        } else {
            return assets[0] || {
                jsFiles: assets.files,
                localJsFiles: assets.localFiles,
                container: assets.container
            };
        }
    };

    /*
     * 动态加载CSS文件列表，可指定页面上的任意位置
     * cssFiles: 来自远程服务器的CSS文件列表(如：/libs/demo.css,lib/demo,lib/demo.css)
     * localCssFiles: 来自本地服务器的CSS文件列表(如：/local/demo.css, local/demo)
     * container: 可不指定将附加到head中，否则将附加到指定的标签位置
    */
    mcs.loadCssAssets = function (/*{cssFiles:[],localCssFiles:[],container:'#containerId'}*/) {
        var assets = handleParam(mcs.config.fileTypes.css, arguments);
        if (!assets.cssFiles.length) return;
        var mergeFiles = [
            { isLocal: false, data: assets.cssFiles },
            { isLocal: true, data: assets.localCssFiles }
        ];

        for (var i = 0, iLen = mergeFiles.length; i < iLen; i++) {
            var file = mergeFiles[i];
            for (var j = 0, jLen = file.data.length; j < jLen; j++) {
                var cssFile = file.data[j];
                var length = cssFile.length;
                if (!length) continue;
                var fileName = getFileName(mcs.config.fileTypes.css, cssFile, file.isLocal);
                var cssElem = document.createElement('link');
                cssElem.setAttribute('rel', 'stylesheet');
                cssElem.setAttribute('href', fileName);

                var container = assets.container || document.getElementsByTagName("head")[0];
                container.appendChild(cssElem);
            }
        }
    };

    /*
    * 动态加载Js文件列表，可指定页面上的任意位置
    * jsFiles: 来自远程服务器的JS文件列表(如：/libs/demo.js,lib/demo,lib/demo.js)
    * localJsFiles: 来自本地服务器的JS文件列表(如：/local/demo.css, local/demo)
    * container: 可不指定将附加到head中，否则将附加到指定的标签位置
   */
    mcs.loadJsAssets = function (/*{jsFiles:[],localJsFiles:[],container:'#containerId'}*/) {
        var params = handleParam(arguments)[0];
        var assets = (params instanceof Object) ? params : {
            jsFiles: params.files,
            localJsFiles: params.localFiles,
            container: params.container
        };
        if (!assets.jsFiles.length) return;
        var mergeFiles = [
            { isLocal: false, data: assets.jsFiles },
            { isLocal: true, data: assets.localJsFiles }
        ];

        for (var i = 0, iLen = mergeFiles.length; i < iLen; i++) {
            var file = mergeFiles[i];
            for (var j = 0, jLen = file.data.length; j < jLen; j++) {
                var jsFile = file.data[j];
                var length = jsFile.length;
                if (!length) continue;
                var fileName = getFileName(mcs.config.fileTypes.javascript, jsFile, file.isLocal);
                var jsElem = document.createElement('script');
                jsElem.setAttribute('src', fileName);

                var container = assets.container || document.getElementsByTagName("head")[0];
                container.appendChild(jsElem);
            }
        }
    };

    /*
    * 对requirejs单独做处理
    * requireFile: 来自远程服务器的RequireJS文件地址(如：libs/require)
    * requireConfig: 来自本地服务器的RequireJS配置文件地址(如：./app/config/require.config)
   */
    mcs.loadRequireJsAssets = function (requireFile, requireConfig) {
        if (!requireFile || !requireConfig) return;
        var fileType = mcs.config.fileTypes.javascript;
        var fileName = getFileName(fileType, requireFile, false);
        var extension = '.' + fileType;
        if (requireConfig.substring(requireConfig.length - extension.length) != extension) {
            requireConfig += extension;
        }
        var jsElem = document.createElement('script');
        jsElem.setAttribute('src', fileName);
        jsElem.setAttribute('data-main', requireConfig);

        document.getElementsByTagName("head")[0].appendChild(jsElem);
    }

    /*
    * JS 产生一个新的GUID随机数
    */
    mcs.newGuid = function () {
        var guid = "";
        for (var i = 1; i <= 32; i++) {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                guid += "-";
        }
        return guid;
    };


    /*
     * 返回当前已选中的操作项集合
     */
    mcs.setSelectedItems = function (items, key, event) {
        var index = items.indexOf(key);

        var isRadio = event.target.type === 'radio';
        if (isRadio) {
            items[0] = key;
            return;
        }
      
        if (event.target.checked) {
            if (index === -1) {
                items.push(key);
            }
        } else {
            if (index !== -1) {
                items.splice(index, 1);
            }
        }
    };

    return mcs;

})();