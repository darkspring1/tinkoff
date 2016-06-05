var gulp = require('gulp');

var rename 			= require("gulp-rename");
var concat			= require('gulp-concat');
var concatCss		= require('gulp-concat-css');
var uglify			= require('gulp-uglify');
var minifyCss 		= require('gulp-cssnano');
var replace      	= require('gulp-replace');

var useref = require('gulp-useref'),
    gulpif = require('gulp-if');


var buildResultDir = './build_result/';

var baseDir = "../";
var jsBundleName = 'bundle.js';
var cssBundleName = 'bundle.css';


function getUniqFilePrefix(){
    return new Date().getTime();
}

gulp.task('build', ['bundle'], function () {

	var cssUniq = getUniqFilePrefix() + cssBundleName;
	var jsUniq = getUniqFilePrefix() + jsBundleName;

	gulp.src(buildResultDir + cssBundleName)
		.pipe(replace('../bootstrap/dist/fonts', 'fonts'))
		.pipe(concatCss(cssUniq))
		.pipe(gulp.dest(buildResultDir));


    gulp.src(buildResultDir + jsBundleName)
           .pipe(replace('# sourceMappingURL=angular-file-upload.js.map', ''))
           .pipe(concat(jsUniq))
           .pipe(gulp.dest(buildResultDir));
		   
	return gulp.src(buildResultDir + 'index.html')
           .pipe(replace(jsBundleName, jsUniq))
		   .pipe(replace(cssBundleName, cssUniq))
		   .pipe(gulp.dest(buildResultDir));
});

gulp.task('bundle', function () {
    return gulp.src(baseDir + 'index.html')
        .pipe(useref())
        .pipe(gulpif('*.js', uglify()))
        .pipe(gulpif('*.css', minifyCss()))
        .pipe(gulp.dest(buildResultDir));
});

gulp.task('default', ['build']);
