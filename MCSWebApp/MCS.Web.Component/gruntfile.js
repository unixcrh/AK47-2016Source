module.exports = function(grunt) {
    grunt.initConfig({

        concat: {
            dist: {
                src: ['src/mcs.js','src/mcs-*/**/*.js'],
                dest: 'libs/mcs-component-1.0.1/mcs.component.js'
            },
			
            css: {
                src: ['src/css/*.css'],
                dest: 'libs/mcs-component-1.0.1/mcs.component.css'
            }
        },
        uglify: {
            buildComponent: {
                src: 'libs/mcs-component-1.0.1/mcs.component.js',
                dest: 'libs/mcs-component-1.0.1/mcs.component.min.js'
            },
			buildGlobal: {
				src: ['src/mcs.config.js', 'src/mcs.global.js'],
				dest: 'src/mcs.global.min.js'
			}
        }
    });
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.registerTask('default', ['concat', 'uglify']);
}
