FROM debian:stable-slim AS installer
RUN apt update && apt install -y curl
RUN curl -sSL https://rover.apollo.dev/nix/v0.28.1 | sh

FROM debian:stable-slim AS runner
COPY --from=installer /root/.rover/bin/rover /root/.rover/bin/rover
ENV PATH="/root/.rover/bin:${PATH}"
RUN apt update && apt install -y ca-certificates && rm -rf /var/lib/apt/lists/* && apt-get clean
ENTRYPOINT [ "/root/.rover/bin/rover" ]