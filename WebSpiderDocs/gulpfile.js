/// <binding />
// include plug-ins

const 	gulp = require('gulp'),
 		
		// concat = require('gulp-concat'),
	
		// minify = require('gulp-minify'),
	
		// del = require('del'),
	
		noop = require("gulp-noop"),
	
		coffee = require('gulp-coffee'),
	
		babel = require('gulp-babel'),
	
		sourcemaps = require('gulp-sourcemaps'),
	
		// watch = require('gulp-watch'),
	
		strip = require('gulp-strip-comments'),
	
		rename = require('gulp-rename'),
		
		// beautify = require('gulp-jsbeautifier'),
		
		plumber = require('gulp-plumber'),
		
		cleanCSS = require('gulp-clean-css'),
		
		autoprefixer = require('gulp-autoprefixer'),
		
		postcss = require('gulp-postcss'),

		sass = require('gulp-sass')(require('sass')),
		
		
		// uglify = require('gulp-uglify'),

		ngAnnotate = require('gulp-ng-annotate'),
		
		removeCode = require('gulp-remove-code');
		


// SCSS
sass.compiler = require('node-sass');

const argv = require('minimist')( process.argv.slice(2) );

console.dir(argv);


//https://gulpjs.com/docs/en/getting-started/watching-files/

var config = {
    //Include all js files but exclude any min.js files
	src: ['./scripts/**/*.coffee', '!./scripts/**/*.min.coffee'],

	cssWatch: ['./content/app/css/**/*.scss'],
	srcCss: ['./content/app/css/custom.scss'],//['./content/**/*.scss'],

	destCssName:'custom.min.css',

	// 	srcJs: ['./scripts/app/controllers/workspace-controller.js'],
	// destJs: '../../website/scripts/app/controllers/'

	 srcJs: ['./scripts/**/*.js'],
	 destJs: '../../website/scripts/'
	//destCss: 'content/app/css/',
	//CssName: 'custom.css'
}


function css()
{
	return gulp.src(config.srcCss)
	.pipe(sass().on('error', sass.logError))
	//.pipe(concat(config.CssName))
	.pipe(cleanCSS({compatibility: 'ie8'}))
	.pipe(postcss(autoprefixer()))
    .pipe(rename(function(path){
		path.basename = config.destCssName.split('.').slice(0, -1).join('.')
	}))
	.pipe(gulp.dest(function (file) {
         return file.base;
     }));
}


gulp.task('sass',css);

// //delete the output file(s)
// gulp.task('clean', function () {
//     //del is an async function and not a gulp plugin (just standard nodejs)
//     //It returns a promise, so make sure you return that from this task function
//     //  so gulp knows when the delete is complete
//     return del(['app/all.min.js']);
// });

// // Combine and minify all files from the app folder
// // This tasks depends on the clean task which means gulp will ensure that the
// // Clean task is completed before running the scripts task.
// gulp.task('scripts',  function () {

//     return gulp.src(config.src)
//       //.pipe(uglify())
//       //.pipe(concat('all.min.js'))
//       .pipe(gulp.dest(function (file) {
//         return file.base;
//     }));
// });

gulp.task('coffee',async function(){

	return gulp.src(config.src)
			.pipe(argv.env == 'dev' ? sourcemaps.init() : noop() )
			.pipe(plumber())
			.pipe(coffee({bare:true}))
			.pipe(argv.env == 'dev'  ?  noop() : strip())
			.pipe(_babel())						
			.pipe(ngAnnotate())
			.pipe(argv.env == 'dev' ? sourcemaps.write('./maps') : noop())
			.pipe(gulp.dest(function (file) {
				return file.base;
			}))
});



async function _dev()
{

	_default();
}

async function _build()
{

	gulp.series('coffee', 'sass');
}

function _watch()
{
	gulp.watch(config.src, gulp.series('coffee'));

	gulp.watch(config.cssWatch, gulp.series('sass'));
}

function _default()
{
	_build();

	_watch();
}

function _babel()
{

	return babel({
		presets: [
			[
				'@babel/preset-env',
				{
					// useBuiltIns: "usage",
					// corejs :'3'
				}
			]
		]
	});
}

exports.dev = _dev;
exports.build = _build;
exports.default = _default;