import axios from 'axios';

enum Endpoints {
    Authorize = 'https://accounts.spotify.com/authorize?',

    RandomPic = '/api/Spotify/GetRandomPic?',
}

export default class Spotify {
    private static readonly clientId = '1b35587a25184c8abd7d16856effe095';
    private static readonly redirectUri = 'https://spot-the-playlist.herokuapp.com/auth-success';

    static readonly AuthLink =
        Endpoints.Authorize
        + 'client_id=' + Spotify.clientId
        + '&redirect_uri=' + Spotify.redirectUri
        + '&response_type=token'
        + '&scope=user-read-playback-state user-library-read';

    Token: string;

    constructor(token: string) {
        if (!token) throw new Error('empty token!');

        this.Token = token;
    }

    get(url: string | Endpoints) {
        return axios.get(url + 'token=' + this.Token);
    }

    async getRandomBackgroundUrl(): Promise<string | null> {
        const {data} = await this.get(Endpoints.RandomPic);

        console.log(data);
        if (!data || !data.backgroundUrl) return null;

        return data.backgroundUrl;
    }
}