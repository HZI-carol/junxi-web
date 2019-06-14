// vue.config.js 配置文档地址：https://cli.vuejs.org/zh/config
module.exports = {
  publicPath: process.env.NODE_ENV === 'production'
    ? '/m/'
    : '/',
  outputDir: 'dist',
  // 静态资源
  assetsDir: 'static',
  // 指定生成的 index.html 的输出路径
  // indexPath: 'index.html',
  runtimeCompiler: true,
  productionSourceMap: false,
  pages: {
    index: {
      // page 的入口
      entry: 'src/main.js',
      // 模板来源
      template: process.env.NODE_ENV === 'production'
        ? 'public/index.html'
        : 'public/index.html',
      // 在 dist/index.html 的输出
      filename: 'index.html',
      // 在这个页面中包含的块，默认情况下会包含
      // 提取出来的通用 chunk 和 vendor chunk。
      chunks: ['chunk-vendors', 'chunk-common', 'index']
    }
  }
}
