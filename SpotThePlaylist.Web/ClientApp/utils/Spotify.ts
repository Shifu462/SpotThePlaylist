import axios from 'axios';

enum SpotifyEndpoints {
    Authorize = 'https://accounts.spotify.com/authorize?',
    CurrentlyPlaying = 'https://api.spotify.com/v1/me/player/currently-playing?',
}

export default class Spotify {
    private static readonly clientId = '1b35587a25184c8abd7d16856effe095';
    private static readonly redirectUri = 'https://spot-the-playlist.herokuapp.com/auth-success';

    static readonly AuthLink =
        SpotifyEndpoints.Authorize
        + 'client_id=' + Spotify.clientId
        + '&redirect_uri=' + Spotify.redirectUri
        + '&response_type=token'
        + '&scope=user-read-playback-state';

    Token: string;

    constructor(token: string) {
        if (!token) throw new Error('empty token!');

        this.Token = token;
    }

    get(url: string | SpotifyEndpoints) {
        return axios.get(url + 'access_token=' + this.Token);
    }

    async getCurrentlyPlaying(): Promise<any> {
        const {data} = await this.get(SpotifyEndpoints.CurrentlyPlaying);

        console.log(data);
        if (!data || !data.item) return null;

        return data.item;
    }
}