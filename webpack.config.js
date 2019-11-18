var path = require('path')
var webpack = require('webpack')
const VueLoaderPlugin = require('vue-loader/lib/plugin')

module.exports = {
	entry: './ClientApp/index.ts',
	output: {
		path: path.resolve(__dirname, 'wwwroot/dist'),
		publicPath: 'wwwroot/dist/',
		filename: 'build.js'
	},
	module: {
		rules: [
			{
				test: /\.vue$/,
				loader: 'vue-loader',
				options: {
					loaders: {
						'less': 'vue-style-loader!css-loader!less-loader'
					}
				}
			},
			{
				test: /\.less$/,
				use: [
					'vue-style-loader',
					'css-loader',
					'less-loader',
					{
						loader: 'style-resources-loader',
						options: {
							patterns: [
								path.resolve(__dirname, './ClientApp/reset.less'),
								path.resolve(__dirname, './ClientApp/common.less')
							]
						}
					}
				]
			},
			{
				test: /\.ts$/,
				loader: 'ts-loader',
				exclude: /node_modules/,
				options: { appendTsSuffixTo: [/\.vue$/] }
			},
			{
				test: /\.css$/,
				loader: 'style-loader!css-loader'
			},
			{
				test: /\.(eot|svg|ttf|woff|woff2)(\?\S*)?$/,
				loader: 'file-loader'
			},
			//{
			//	test: /\.(png|jpg|gif|svg)$/,
			//	loader: 'file-loader',
			//	options: {
			//		name: '[name].[ext]?[hash]'
			//	}
			//}
		]
	},
	plugins: [
		new VueLoaderPlugin()
	],
	resolve: {
		extensions: ['.ts', '.js', '.vue', '.json'],
		alias: {
			'vue$': 'vue/dist/vue.esm.js'
		}
	},
	devServer: {
		historyApiFallback: true,
		noInfo: true
	},
	performance: {
		hints: false
	},
	devtool: '#eval-source-map'
}

// if (process.env.NODE_ENV === 'production') {
// 	module.exports.devtool = '#source-map'
// 	// http://vue-loader.vuejs.org/en/workflow/production.html
// 	module.exports.plugins = (module.exports.plugins || []).concat([
// 		new webpack.DefinePlugin({
// 			'process.env': {
// 				NODE_ENV: '"production"'
// 			}
// 		}),
// 		new webpack.optimize.UglifyJsPlugin({
// 			sourceMap: true,
// 			compress: {
// 				warnings: false
// 			}
// 		}),
// 		new webpack.LoaderOptionsPlugin({
// 			minimize: true
// 		})
// 	])
// }