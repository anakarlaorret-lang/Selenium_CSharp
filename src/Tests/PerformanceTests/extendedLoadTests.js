import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
  vus: 50,            // 50 concurrent users
  duration: '2m',     // run for 2 minutes
  thresholds: {
    http_req_duration: ['p(95)<1000'], // 95% < 1s
    http_req_failed: ['rate<0.01'],
  },
};

export default function () {
  http.get('https://towersoft.net/careers');
  sleep(1);
}