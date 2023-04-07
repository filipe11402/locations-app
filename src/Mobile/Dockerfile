FROM node:13-alpine as build
COPY package*.json /app/

EXPOSE 8100

WORKDIR /app
RUN npm install -g ionic
RUN npm install

COPY . /app/
CMD ["ionic", "serve", "--external --host=127.0.0.1 --port=8100 --ssl"]
