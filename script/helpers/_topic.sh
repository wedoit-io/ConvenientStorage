topic() {
  title="$1" && shift
  if [[ -z "$*" ]]; then
    echo "-----> $title"
  else
    echo "-----> $title: $*"
  fi
}
