define([], function () {
    var states = {
        'student': {
            url: '/student',
            templateUrl: 'app/student/student-list/student-list.html',
            controller: 'studentListController',
            dependencies: ['app/student/student-list/student-list.controller.js']
        },
        'student.add': {
            url: '/add',
            templateUrl: 'app/student/student-add/student-add.html',
            controller: 'studentAddController',
            dependencies: ['app/student/student-add/student-add.controller.js']
        }
    };

    return states;
});
