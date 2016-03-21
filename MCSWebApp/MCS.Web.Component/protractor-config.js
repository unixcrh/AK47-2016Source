// An example configuration file.
exports.config = {
    directConnect: true,
    allScriptsTimeout: 99999,

    // The address of a running selenium server.
    seleniumAddress: 'http://localhost:4444/wd/hub',
    // Capabilities to be passed to the webdriver instance.
    capabilities: {
        'browserName': 'chrome'
    },
    chromeOnly: true,
    onPrepare: function() {
        // var jasmineReporters = require('jasmine-reporters');

        // //update proper path
        // jasmine.getEnv().addReporter(
        //     new jasmineReporters.JUnitXmlReporter({
        //         savePath: 'testReport',
        //         consolidateAll: false
        //     })
        // );

    },

    // Framework to use. Jasmine is recommended.
    framework: 'jasmine',
    baseUrl: 'http://localhost:8082/',

    // Spec patterns are relative to the current working directly when
    // protractor is called.
    specs: ['js/testUI/*.js'],

    // Options to be passed to Jasmine.
    jasmineNodeOpts: {
        showColors: true,
        defaultTimeoutInterval: 30000,
        isVerbose: true,
        includeStackTrace: true
    }
};
