# Example usage: `echo "Lorem ipsum" | indent`
indent() {
  sed -u 's/^/       /'
}
