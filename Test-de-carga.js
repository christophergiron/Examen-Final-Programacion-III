import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    stages: [
        { duration: '20s', target: 10 },
        { duration: '30s', target: 50 },
        { duration: '30s', target: 100 },
        { duration: '20s', target: 0 },
    ],
    thresholds: {
        http_req_failed: ['rate<0.1'],
        http_req_duration: ['p(95)<5000'],
    }
};

const BASE_URL = 'http://localhost:5043/api';

const USER_IDS = [
    '42be1b74-ee6a-4d3a-b8f6-d8cefd182cb6'
];

const PRODUCT_IDS = [1];

export default function () {

    let userId = USER_IDS[Math.floor(Math.random() * USER_IDS.length)];
    let productId = PRODUCT_IDS[Math.floor(Math.random() * PRODUCT_IDS.length)];

    let res1 = http.post(
        `${BASE_URL}/Carrito/${userId}/add?productId=${productId}&cantidad=1`
    );

    check(res1, {
        'add to cart ok': (r) => r.status === 200
    });

    sleep(0.5);

    let res2 = http.get(
        `${BASE_URL}/Carrito/usuario/${userId}`
    );

    check(res2, {
        'get cart ok': (r) => r.status === 200
    });

    sleep(0.5);

    let res3 = http.post(
        `${BASE_URL}/Carrito/checkout-async?userId=${userId}`
    );

    check(res3, {
        'checkout ok': (r) => r.status === 200
    });

    sleep(1);
}