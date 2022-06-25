import { check, sleep } from 'k6';
import http from 'k6/http';
import { Counter, Rate } from 'k6/metrics';

let ErrorCount = new Counter('errors');
let ErrorRate = new Rate('error_rate');

export let options = {

    stages: [
        { duration: "5s", target: 100 },
        { duration: "15s", target: 100 },
        { duration: "10s", target: 0 }
    ],
    thresholds: {
        error_rate: ['rate<0.1']
    }
}
export default function () {
    var params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };
    const number= Math.floor(Math.random() * 10000) + 1;
    const url=""
    var payload = JSON.stringify({
        id: "3873E359-CB58-4C29-A637-5C445C81C84D",
        name: "string"+number,
        category: "string"+number,
        price: number
    });
    //http.post(url, payload, params);
    let res = http.put('https://localhost:7218/api/ProductEf6Temporal',payload, params);
    let success = check(res, { "status is 200": r => r.status == 200 })
    if (!success) {
        ErrorCount.add(1);
        ErrorRate.add(true);
    }
    else {
        ErrorRate.add(false);
    }
    sleep(0.5);
}