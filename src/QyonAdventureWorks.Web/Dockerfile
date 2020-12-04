FROM node:14-alpine

WORKDIR /app

COPY ./package.json .
COPY ./package-lock.json .

RUN npm install
RUN npm install -g serve

COPY . .

RUN npm run build

EXPOSE 80
CMD [ "serve", "-s", "-l", "80", "build" ]