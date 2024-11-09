import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SentimentService {
  private scoringUri = 'https://textmining.azure-api.net/sentiment_model;rev=2/score'; // Updated with revision 2

  constructor(private http: HttpClient) { }

  getSentiment(inputText: string): Observable<any> {
    const params = new HttpParams()
      .set('input_text', inputText);  // Using query parameters instead of a request body

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Ocp-Apim-Subscription-Key': 'ca4021eab41244b8858fd0b32b28e33a' // Replace with your actual subscription key,
    });

    return this.http.get<any>(this.scoringUri, { headers, params });
  }
}
