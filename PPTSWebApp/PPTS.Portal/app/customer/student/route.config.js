define([], function () {
    var states = {
        'student': {
            url: '/student',
            templateUrl: 'app/customer/student/student-list/student-list.html',
            controller: 'studentListController',
            dependencies: ['app/customer/student/student-list/student-list.controller']
        },
        'student.add': {
            url: '/add',
            templateUrl: 'app/customer/student/student-add/student-add.html',
            controller: 'studentAddController',
            dependencies: ['app/customer/student/student-add/student-add.controller']
        }
    };

    return states;
});
