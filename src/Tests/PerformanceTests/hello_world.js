import http from 'k6/http';

export default function () {
  http.get('https://test.k6.io'); // simple public test site
}