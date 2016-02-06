if (typeof (wf) == "undefined")
    wf = {};

wf.nodeElapsedParser = function (v) {
    var result = "yellow";

    switch (v) {
        case true:
        case "true":
            result = "green";
            break;
    }

    return result;
}

wf.linkElapsedParser = function (v) {
    var result = "gray";

    switch (v) {
        case true:
        case "true":
            result = "green";
            break;
    }

    return result;
}

wf.returnLinkParser = function (v) {
    var result = null;

    switch (v) {
        case true:
        case "true":
            result = [5, 5];
            break;
    }

    return result;
}

wf.nodeToolTip = function (model) {
    var descriptors = [];

    descriptors.push({ label: "活动ID", description: model.id });
    descriptors.push({ label: "活动Key", description: model.key });
    descriptors.push({ label: "活动名称", description: model.name });

    return descriptors;
}