ARG NODE_VERSION=24.12.0-alpine

FROM node:${NODE_VERSION} AS dev

ENV NODE_ENV=development

WORKDIR /app

# Run as the unprivileged node user so node_modules is node-owned (no recursive chown needed).
RUN chown node:node /app
USER node

COPY --chown=node:node package.json package-lock.json* ./

RUN --mount=type=cache,target=/home/node/.npm,uid=1000,gid=1000 npm install

COPY --chown=node:node . .

EXPOSE 5173

CMD [ "npm", "run", "dev", "--", "--host" ]