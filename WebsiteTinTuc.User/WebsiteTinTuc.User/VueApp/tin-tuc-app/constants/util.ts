import URL from '../constants/url'

class Util {
    getLinkPath(url: string) {
        if(!url)
            return "";
        if (!url.startsWith('http') && !url.startsWith('https')) {
            if (url.startsWith('/')) {
                url = url.slice(1);
                return URL + url;
            }
            return URL + url;
        }
        else return url;
    }
}

const util = new Util();
export default util;