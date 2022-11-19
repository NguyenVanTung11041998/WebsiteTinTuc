const URL =
	process.env.NODE_ENV === 'production'
		? 'http://localhost:21021/'
		: 'http://10.50.10.80:21021/';
export default URL;
