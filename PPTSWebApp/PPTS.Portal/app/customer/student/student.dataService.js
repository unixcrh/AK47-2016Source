define(['customer'], function (customer) {

    customer.registerFactory('studentDataService', ['$resource', 'pptsConfiguration', function ($resource, pptsConfiguration) {

        var resource = $resource(pptsConfiguration.customerApiServerPath + 'api/students/:operation/:id', { operation: '@operation', id: '@id' }, { 'post': { method: 'POST' } });

        resource.getAllStudents = function (criteria, success, error) {
            resource.post({ operation: 'getAllStudents' }, criteria, success, error);
        }

        resource.getStudent = function (studentId) {
            resource.query({ operation: 'getStudentBaseinfo', id: studentId }, function (studentId) {

            });
        };

        resource.addStudent = function () {
            resource.query({ operation: 'createStudent' }, function (student) {

            });
        }

        resource.editStudent = function (studentId) {
            resource.query({ operation: 'updateStudent', id: studentId }, function (student) {

            });
        }

        resource.saveStudent = function (student, studentId) {
            if (!studentId) {
                resource.save({ operation: 'createStudent' }, student, function (result) {

                });
            } else {
                resource.save({ operation: 'updateStudent', id: studentId }, student, function (result) {

                });
            }
        };


        return resource;
    }]);
});
