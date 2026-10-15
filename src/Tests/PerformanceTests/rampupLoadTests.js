import http from 'k6/http';

export let options = {
  stages: [
    { duration: '30s', target: 10 },   // start with 10 users
    { duration: '30s', target: 30 },   // ramp up to 30
    { duration: '30s', target: 50 },   // ramp up to 50
    { duration: '30s', target: 10 },   // ramp down to 10
  ],
  thresholds: {
    http_req_duration: ['p(95)<1000'],
    http_req_failed: ['rate<0.01'],
  },
};

export default function () {
  http.get('https://towersoft.net/careers');
}